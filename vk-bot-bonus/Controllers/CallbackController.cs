using Microsoft.AspNetCore.Mvc;
using vk_bot_bonus.Dto;
using vk_bot_bonus.Models;
using vk_bot_bonus.Service;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;

namespace vk_bot_bonus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallbackController : ControllerBase
{
    
    private readonly VkApi _vkApi;
    private readonly ExcelService _excelService;
    private readonly List<UserData> _userScores;

    public CallbackController(VkApi vkApi, ExcelService excelService)
    {
        _vkApi = vkApi;
        _excelService = excelService;
        _userScores = _excelService.GetUserScores();
    }

    [HttpPost]
    public IActionResult Callback([FromBody] GroupUpdate update)
    {
        if (update.Type == GroupUpdateType.MessageNew)
        {
            var message = update.MessageNew.Message;
            var response = ProcessMessage(message.Text);
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = message.PeerId.Value,
                Message = response
            });
        }

        return Ok("ok");
    }

    private string ProcessMessage(string messageText)
    {
        var parts = messageText.Split(' ');
        if (parts.Length != 2) return "Пожалуйста, введите имя и фамилию.";

        var firstName = parts[0];
        var lastName = parts[1];

        var userScore = _userScores.FirstOrDefault(u => 
            u.FirstName.Equals(firstName, System.StringComparison.OrdinalIgnoreCase) && 
            u.LastName.Equals(lastName, System.StringComparison.OrdinalIgnoreCase));

        return userScore != null ? $"{firstName} {lastName}, Ваши баллы: {userScore.Score}" : "Пользователь не найден.";
    }
}