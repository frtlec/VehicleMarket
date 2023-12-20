using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.Core;
namespace VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels
{
    public class AdvertModel : EntityBase, IAggregateRoot
    {
        public int MemberId { get; set; }

        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public double KM { get; private set; }
        public string Color { get; private set; }
        public string Gear { get; private set; }
        public string Fuel { get; private set; }
        public string FirstPhoto { get; private set; }
        public string SecondPhoto { get; private set; }
        public string UserInfo { get; private set; }
        public string UserPhone { get; private set; }
        public string Text { get; private set; }
        public int CityId { get; private set; }
        public string CityName { get; private set; }
        public City City { get; private set; }
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public CategoryModel Category { get; private set; }


        public int TownId { get; private set; }
        public string TownName { get; private set; }
        public TownModel Town { get; private set; }

        public int ModelId { get; private set; }
        public string ModelName { get; private set; }
        public Model Model { get; private set; }

        public AdvertModel(int memberId,int year, decimal price, string title, DateTime date, double kM, string color, string gear, string fuel, string firstPhoto, string secondPhoto, string userInfo, string userPhone, string text, CategoryModel category, TownModel town, Model model, City city)
        {
            MemberId=memberId;
            Year = year;
            Price = price;
            Title = title;
            Date = date;
            KM = kM;
            Color = color;
            Gear = gear;
            Fuel = fuel;
            FirstPhoto = firstPhoto;
            SecondPhoto = secondPhoto;
            UserInfo = userInfo;
            UserPhone = userPhone;
            Text = text;
            CategoryId = category.Id;
            CategoryName = category.Name;
            Category = category;
            TownId = town.Id;
            TownName = town.Name;
            Town = town;
            ModelId = model.Id;
            ModelName = model.Name;
            Model = model;
            CityId = city.Id;
            CityName = city.Name;   
        }
    }
}
