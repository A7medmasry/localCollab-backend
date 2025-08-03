using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.OutDto.User
{
    public class StatusUserOutDto
    {
        public string TopRated { get; set; }
        public bool TopRatedStatus { get; set; }
        public string Reliable { get; set; }
        public bool ReliableStatus { get; set; }
        public string FastResponder { get; set; }
        public bool FastResponderStatus { get; set; }
        public double ResponseRate { get; set; }
        public double ShowUpRate { get; set; }
    }
}
