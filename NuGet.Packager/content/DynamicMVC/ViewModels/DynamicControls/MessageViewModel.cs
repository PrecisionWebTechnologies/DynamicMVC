using DynamicMVC.Shared.Enums;

namespace DatabaseManagerMVC.UI.DynamicMVC.ViewModels.DynamicControls
{
    public class MessageViewModel
    {
        public MessageViewModel()
        {
            MessageTypeEnum = MessageTypeEnum.Success;
        }

        public MessageViewModel(string message)
        {
            Message = message;
        }

        public MessageViewModel(MessageTypeEnum messageTypeEnum, string message): this(message)
        {
            MessageTypeEnum = messageTypeEnum;
        }

        public MessageTypeEnum MessageTypeEnum { get; set; }
        public string Message { get; set; }
    }
}