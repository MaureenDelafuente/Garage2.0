using Garage2._0.Models.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels.FeedbackViewModels
{
    public class FeedbackListViewModel
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Rating { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FeedbackMessage { get; set; } = string.Empty;
    }
}
