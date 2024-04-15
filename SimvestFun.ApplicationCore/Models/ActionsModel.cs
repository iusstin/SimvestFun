namespace SimvestFun.ApplicationCore.Models
{
    public class ActionsModel
    {
        public List<UserActionModel> UserActions { get; set; } = new List<UserActionModel>();
        public int AllActionsCount { get; set; }
    }
}
