using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class ShoppingCartModel
    {
        //creating delegate. to mention what discount is in some way.
        //it will not return anything. this is definition for delegate. like interface
        //DELEGATES ARE BASICALLY methods that you pass around and can execute in other locations
        public delegate void MentionDiscount(decimal subTotal);
        public List<ProductModel> Items { get; set; } = new List<ProductModel>();

        //as parameter MentionDiscount delegate. Second param is delegate Func
        //in func first passing List of ProductModal. have whole list in case i want to do 
        //something besides subtotal. like count o r anything else. then pass subtotal(decimal)
        //then output type decimal. which is total. Func is method that has return value other than void. and naming it
        public decimal GenerateTotal(MentionDiscount mentionSubtotal,
            Func<List<ProductModel>,decimal,decimal> calculateDiscountedTotal,
            Action<string> tellUserWeAreDiscounting)
        {
            //fOR Func we dont have to diclare it. dont have to create signature

            //Sum uses delegate too. Func<ProductModal, decimal>>. 
            decimal subTotal = Items.Sum(x => x.Price);
            //passing to mentionDiscount method a subTotal. in this method it it has no clue where its located or what it does
            //and its good. 
            mentionSubtotal(subTotal);

            //alert user that we are discounting
            tellUserWeAreDiscounting("We are applying your discount");

            //calling passed Func deleage method. passing Items array and subtotal(decimal). it returns total(decimal)
            decimal total =  calculateDiscountedTotal(Items, subTotal);
            return total;
        }
    }
}
