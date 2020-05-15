using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
    public class _ShoppingCartContentModel : PageModel
    {
        #region Properties
        private double DeliveryPrice { get; set; }
        private double OrderPrice { get; set; }
        private double SubTotal { get; set; }
        private ICollection<ProductOrder> ProductOrders { get; set; }
        public double TotalPrice { get; set; }
        #endregion


        #region Constructor

        public _ShoppingCartContentModel(ICollection<ProductOrder> productOrder)
        {
            ProductOrders = productOrder;
        }

        #endregion

        #region Methods

        public double GetOrderPrice(ProductOrder order, Product product)
        {
            double orderPrice = ((double)order.Quantity) * product.Price;

            this.OrderPrice = orderPrice;

            return ((double)order.Quantity) * product.Price;
        }

        public int GetQuantity(ProductOrder order)
        {
            if (order.Quantity <= 0)
            {
                order.Quantity = 1;
            }
            return order.Quantity;
        }

        public ICollection<ProductOrder> GetProductOrders()
        {
            return ProductOrders;
        }

        public double GetDeliveryPrice()
        {
            //todo: Get a real delivery price by CPF
            double deliveryPrice = 20;

            this.DeliveryPrice = deliveryPrice;

            return deliveryPrice;
        }

        public double GetSubtotal()
        {
            double subTotal = 0;
            foreach (var order in ProductOrders)
            {
                double orderprice = 0;
                foreach (Product product in order.Products)
                {
                    orderprice += product.Price;
                }
                orderprice *= order.Quantity;

                subTotal += orderprice; 
            }

            SubTotal = subTotal; 

            return SubTotal; 
        }

        public double GetTotalPrice()
        {
            double totalPrice = DeliveryPrice + SubTotal;

            this.TotalPrice = totalPrice; 

            return TotalPrice;
        }
    } 
    #endregion
}