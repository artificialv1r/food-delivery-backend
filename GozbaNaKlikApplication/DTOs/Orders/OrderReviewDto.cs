using GozbaNaKlikApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Orders
{
    public class OrderReviewDto
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int RestaurantGrade { get; set; }
        public string? RestaurantComment { get; set; }

        [Range(1, 5)]
        public int CourierGrade { get; set; }
        public string? CourierComment { get; set; }

    }
}
