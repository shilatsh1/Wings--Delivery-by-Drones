﻿using System;
using System.Collections.Generic;
using IBL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        enum MenuOptions { Exit, Add, Update, ViewOne, ViewList };
        enum AddOrView { BaseStation = 1, Drone, Customer, Parcel };
        enum UpDate { DroneName = 1, BaseStation, Customer, ChargeDrone, DisChargeDrone, ParcelToDrone, ParcelCollect, ParcelDelivered };
        enum ViewList { BaseStation = 1, Drone, Customer, Parcel, NotConnected, AvaliableSlots };
        static void Main(string[] args)
        {
            IBL.BL bl = new IBL.BL();
            Drone d = new Drone();
            Parcel p = new Parcel();
            BaseStation b = new BaseStation();
            Customer c = new Customer();
            Console.WriteLine(@"Enter a number:          
0- Exit,
1- To add,
2- To update,
3- To print,
4- To print all.");
            MenuOptions mo;
            int option;
            bool check = int.TryParse(Console.ReadLine(), out option);
            mo = (MenuOptions)option;
            //if (check)                                         
            //    mo = (MenuOptions)option                                         
            //else                                     
            //    Console.WriteLine("ERROR");
            while (mo != 0)
            {
                switch (mo)
                {
                    case MenuOptions.Exit:
                        break;
                    case MenuOptions.Add:
                        Console.WriteLine(@"Enter a number:          
1- To add a base station,
2- To add a drone,
3- To add a customer,
4- To add a parcel.");
                        AddOrView aov;
                        check = int.TryParse(Console.ReadLine(), out option);
                        aov = (AddOrView)option;
                        switch (aov)
                        {
                            case AddOrView.BaseStation:
                                Console.WriteLine("Enter id, name, location and num of Charge slots:");
                                b.Id = int.Parse(Console.ReadLine());
                                b.Name = Console.ReadLine();
                                b.Place = new Location();
                                b.Place.Longitude = Convert.ToDouble(Console.ReadLine());
                                b.Place.Latitude = Convert.ToDouble(Console.ReadLine());
                                b.AvaliableSlots = int.Parse(Console.ReadLine());
                                bl.AddStation(b);
                                break;
                            case AddOrView.Drone:
                                Console.WriteLine("Enter id, model, max weight and station num of a drone");
                                d.Id = int.Parse(Console.ReadLine());
                                d.Model = Console.ReadLine();
                                d.Weight = (WeightCategories)int.Parse(Console.ReadLine());
                                int num = int.Parse(Console.ReadLine());
                                b = bl.GetStation(num);
                                d.CurrentPlace = new Location();
                                d.CurrentPlace.Longitude = b.Place.Longitude;
                                d.CurrentPlace.Latitude = b.Place.Latitude;
                                bl.AddDrone(d);
                                break;
                            case AddOrView.Customer:
                                Console.WriteLine("Enter id, name, phone number, and location of a customer");
                                c.Id = int.Parse(Console.ReadLine());
                                c.Name = Console.ReadLine();
                                c.PhoneNum = Console.ReadLine();
                                c.Place = new Location();
                                c.Place.Longitude = Convert.ToDouble(Console.ReadLine());
                                c.Place.Latitude = Convert.ToDouble(Console.ReadLine());
                                bl.AddCustomer(c);
                                break;
                            case AddOrView.Parcel:
                                Console.WriteLine("Enter sender id, receiver id, weight and priority of a parcel");
                                p.Sender = new CustomerInP();
                                p.Sender.Id = int.Parse(Console.ReadLine());
                                p.Receiver = new CustomerInP();
                                p.Receiver.Id= int.Parse(Console.ReadLine());
                                p.Weight = (WeightCategories)int.Parse(Console.ReadLine());
                                p.Priority = (Priorities)int.Parse(Console.ReadLine());
                                bl.AddParcel(p);
                                break;
                            default:
                                break;
                        }
                        break;
                        case MenuOptions.Update:
                        Console.WriteLine(@"Enter a number:          
1- To update drone model,
2- To update a base station,
3- To update a customer,
4- To update drone to charge,
5- To update discharge drone,
6- To update parcel to drone,
7- To update parcel collect,
8-To update parcel delivered.");
                        int Id, NumSlots;
                        string Model, Name, phoneNum ;
                        double time;
                        UpDate up;
                        check = int.TryParse(Console.ReadLine(), out option);
                        up = (UpDate)option;
                        switch (up)
                        {
                            case UpDate.DroneName:
                                Console.WriteLine("Enter Drone id and model");
                                Id = int.Parse(Console.ReadLine());
                                Model = Console.ReadLine();
                                bl.UpdateDroneName(Id, Model);
                                break;
                            case UpDate.BaseStation:
                                Console.WriteLine("Enter id, name and num slots of a base station");
                                Id = int.Parse(Console.ReadLine());
                                Name = Console.ReadLine();
                                int.TryParse(Console.ReadLine(), out NumSlots);
                                bl.UpdateStation(Id, Name, NumSlots);
                                break;
                            case UpDate.Customer:
                                Console.WriteLine("Enter id, name and phone number of a customer");
                                Id= int.Parse(Console.ReadLine());
                                Name = Console.ReadLine();
                                phoneNum= Console.ReadLine();
                                bl.UpdateCustomer(Id, Name, phoneNum);
                                break;
                            case UpDate.ChargeDrone:
                                Console.WriteLine("Enter drone id");
                                Id = int.Parse(Console.ReadLine());
                                bl.UpdateDroneToCharge(Id);
                                break;
                            case UpDate.DisChargeDrone:
                                Console.WriteLine("Enter drone id and Charging time");
                                Id = int.Parse(Console.ReadLine());
                                time= Convert.ToDouble(Console.ReadLine());
                                bl.UpdateDisChargeDrone(Id, time);
                                break;
                            case UpDate.ParcelToDrone:
                                Console.WriteLine("Enter drone id");
                                Id = int.Parse(Console.ReadLine());
                                bl.UpdateParcelToDrone(Id);
                                break;
                            case UpDate.ParcelCollect:
                                Console.WriteLine("Enter drone id");
                                Id = int.Parse(Console.ReadLine());
                                bl.UpdateParcelCollect(Id);
                                break;
                            case UpDate.ParcelDelivered:
                                Console.WriteLine("Enter drone id");
                                Id = int.Parse(Console.ReadLine());
                                bl.UpdateParcelProvide(Id);
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }
    }
}