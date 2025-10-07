﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SuperShop.Data.Entities
{
    public class Order : IEntity
    {

        public int Id { get; set; } //MUITOS PARA UM

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode =false)]
        public DateTime OrderDate { get; set; } //MUITOS PARA UM


        [Required]
        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; } //


        [Required]
        public User User { get; set; }

        
        public IEnumerable<OrderDetail> Items{ get; set; }


        //quantidade de produtos
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => Items == null ? 0 : Items.Count(); //propriedade so de leitura numero de itens 




        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity);



        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);


        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime? OrderDateLocal => this.OrderDate == null ? null : this.OrderDate.ToLocalTime();

    }
}
