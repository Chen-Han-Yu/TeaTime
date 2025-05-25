using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeaTime.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //可以新增要顯示的名稱
        [MaxLength(30)] //限制字串長度
        [DisplayName("類別名稱")]
        public required string Name { get; set; }
        [DisplayName("顯示排序")]
        [Range(0, 200,ErrorMessage = "輸入錯誤！範圍應該要在1-200之間")] //限制數字範圍
        public int DisplayOrder { get; set; }
    }
}
