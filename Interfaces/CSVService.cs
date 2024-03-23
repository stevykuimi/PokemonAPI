using CsvHelper;
using PokemonAPI.Models;

namespace PokemonAPI.Interfaces
{
    public class CSVService : ICSVService
    {
        private readonly string _csvFilePath;
        public CSVService()
        {
        }
        public CSVService(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public List<Pokemon> GetAllData()
        {
            using (var reader = new StreamReader(_csvFilePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var response = csv.GetRecords<Pokemon>().ToList();

                return response;
            }
        }

        public Pokemon GetDataById(int id)
        {
            using (var reader = new StreamReader(_csvFilePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var data = csv.GetRecords<Pokemon>().FirstOrDefault(x => x.Id == id);
                if (data is not null)
                {
                    return data;
                }
                else
                {
                    throw new KeyNotFoundException("Data not found with the provided id.");
                }

            }
        }

        public void AddData(Pokemon data)
        {
            var records = GetAllData();
            data.Id = records.Count > 0 ? records.Max(x => x.Id) + 1 : 1;

            records.Add(data);

            WriteToCSV(records);
        }

        public void UpdateData(int id, Pokemon newData)
        {
            var records = GetAllData();
            var existingData = records.FirstOrDefault(x => x.Id == id);
            if (existingData != null)
            {
                existingData.Name = newData.Name; // Update other properties similarly

                WriteToCSV(records);
            }
            else
            {
                throw new KeyNotFoundException("Data not found with the provided id.");
            }
        }

        public void DeleteData(int id)
        {
            var records = GetAllData();
            var dataToRemove = records.FirstOrDefault(x => x.Id == id);
            if (dataToRemove != null)
            {
                records.Remove(dataToRemove);

                WriteToCSV(records);
            }
            else
            {
                throw new KeyNotFoundException("Data not found with the provided id.");
            }
        }

        private void WriteToCSV(List<Pokemon> records)
        {
            using (var writer = new StreamWriter(_csvFilePath))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
    }
}