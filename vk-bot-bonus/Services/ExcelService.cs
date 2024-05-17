using Aspose.Cells;
using vk_bot_bonus.Models;

namespace vk_bot_bonus.Service;

public class ExcelService
{
    private readonly string _filePath;

    public ExcelService(string filePath)
    {
        _filePath = filePath;
    }

    public List<UserData> GetUserScores()
    {
        var userScores = new List<UserData>();

        var workbook = new Workbook(_filePath);
        var worksheet = workbook.Worksheets[0];
        var cells = worksheet.Cells;

        for (int row = 1; row <= cells.MaxDataRow; row++)
        {
            var firstName = cells[row, 0].StringValue;
            var lastName = cells[row, 1].StringValue;
            var score = cells[row, 2].IntValue;

            userScores.Add(new UserData
            {
                FirstName = firstName,
                LastName = lastName,
                Score = score
            });
        }

        return userScores;
    }
}