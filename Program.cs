﻿using ConsoleProjectCodeAcademy.Exceptions;
using ConsoleProjectCodeAcademy.Exceptions.Validations;
using ConsoleProjectCodeAcademy.Helpers;
using ConsoleProjectCodeAcademy.Models;
using ConsoleProjectCodeAcademy.Services;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace ConsoleProjectCodeAcademy
{
    public class Program
    {
        static void Main(string[] args)
        {
            User activeUser = new("", "", "");
            UserService userService = new UserService();
            CategoryService categoryService = new CategoryService();

            MedicineService medicineService = new MedicineService();
            bool exit = false;

            while (!exit)
            {

            restart:

                
                Console.WriteLine("Menu: ");
                Console.WriteLine("1.User Registration");
                Console.WriteLine("2.User Login");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();

                Console.Clear();
                switch (choice)
                {

                    case "1"://registration
                    registration:
                        Console.WriteLine("Please, enter fullname: ");
                        string fullname = Console.ReadLine();
                        if (!(fullname.IsFullnameValidate()))
                        {
                            Helper.Print("Please, enter first letter uppercase or lenght of fullname is bigger than " +
                                "2 and lower than 22", ConsoleColor.Red);
                            goto registration;
                        }
                        else
                        {

                            Console.WriteLine(fullname);
                        }
                    email:
                        Console.WriteLine("Please, enter email: ");
                        string email = Console.ReadLine();

                        if (!email.IsEmailValidate())
                        {
                            Helper.Print("Username of gmail should be contains '@' symbol and the lenght of username should" +
                                "be bigger is 2 and lower is 22", ConsoleColor.Red);
                            goto email;

                        }
                        else
                        {
                            Console.WriteLine(email);
                        }
                    password:
                        Console.WriteLine("Please, enter password: ");
                        string password = Console.ReadLine();

                        if (!password.IsPasswordValidate())
                        {
                            Helper.Print("Length of password should be 8 and should be contain big letter", ConsoleColor.Red);
                            goto password;
                        }
                        else
                        {
                            Console.WriteLine(password);
                        }
                        try
                        {
                            User user = new User(fullname, email, password);
                            userService.AddUser(user);
                        }
                        catch (Exception e)
                        {
                            Helper.Print(e.Message, ConsoleColor.Red);
                        }



                        goto restart;


                    case "2":

                        Console.WriteLine("Do you have an account? (yes/no)");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "no")
                        {
                            Console.WriteLine("Please, register...");
                            goto registration;
                        }
                        else if (answer == "yes")
                        {

                            Console.WriteLine("Enter email: ");
                            string loginEmail = Console.ReadLine();
                            Console.WriteLine("Enter password: ");
                            string loginPassword = Console.ReadLine();



                            try
                            {
                                activeUser = userService.Login(loginEmail, loginPassword);
                                Helper.Print($"{activeUser.Fullname} welcome!", ConsoleColor.Green);
                                // Console.WriteLine($"{activeUser.Fullname} welcome!");

                            }
                            catch (NotFoundException ex)
                            {

                                Helper.Print(ex.Message, ConsoleColor.Red);

                                goto restart;
                            }
                        }
                        else
                        {
                            Helper.Print("Invalid input.Please enter 'yes' or 'no'", ConsoleColor.Red);
                            goto restart;
                        }

                        break;

                    case "0":
                        exit = true;
                        return;
                    default:
                        Helper.Print("FALSE CHOICE...Please, try again...", ConsoleColor.Red);
                        goto restart;



                }
                bool userExit = false;
                while (!userExit)
                {
                    Console.WriteLine("Please, select the choice: ");
                    Console.WriteLine("1.Create new category");
                    Console.WriteLine("2.Show all medicines");
                    Console.WriteLine("3.Show medicines for their categories");
                    Console.WriteLine("4.Show medicines for their id");
                    Console.WriteLine("5.Show medicines for their name");
                    Console.WriteLine("6.Create new medicine");
                    Console.WriteLine("7.Remove medicine");
                    Console.WriteLine("8.Update medicine");
                    Console.WriteLine("9.Exit program");
                    Console.WriteLine("10.Show all categories");
                    Console.WriteLine("Enter your choice: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1"://create category
                            try
                            {
                                Helper.Print("\nEnter the name of category: ", ConsoleColor.Magenta);

                                string categoryName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(categoryName))
                                {
                                    throw new IsNullException("Bosluq olmaz");
                                }
                                Category newCategory1 = new Category(categoryName);
                                categoryService.CreateCategory(newCategory1);
                                Helper.Print("Category succesfully created!", ConsoleColor.Green);
                                // Console.WriteLine("Category succesfully created!");
                            }
                            catch (Exception ex)
                            {
                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);
                            }
                            break;


                        case "2":
                            Helper.Print("\nAll medicines: ", ConsoleColor.Magenta);
                            // Console.WriteLine("\nAll medicines: ");
                            medicineService.GetAllMedicines(activeUser.Id);


                            break;
                        case "3"://category  SORUS duzdurmu  BAXXXXmaa
                            try
                            {
                                Helper.Print("\nEnter category id: ", ConsoleColor.Magenta);

                                int categorySearchId = int.Parse(Console.ReadLine());
                                Helper.Print($"Category id {categorySearchId} medicines: ", ConsoleColor.Magenta);

                                medicineService.GetMedicineByCategory(categorySearchId);
                                foreach (var Medicine in DB.Medicines)
                                {
                                    if (Medicine.Id == activeUser.Id)
                                    {
                                        Console.WriteLine(Medicine);

                                    }
                                }

                            }
                            catch (NotFoundException ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);

                            }
                            catch (Exception ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);

                            }

                            break;
                        case "4"://id
                            try
                            {
                                Helper.Print("\nEnter the user id: ", ConsoleColor.Magenta);

                                int searchId;
                            restartPrice:
                                bool result = int.TryParse(Console.ReadLine(), out searchId);

                                if (result == false)
                                {
                                    Helper.Print("Duzgun deyer qeyd edin", ConsoleColor.Red);

                                    goto restartPrice;
                                }
                                Helper.Print($"The medicines with {searchId}:", ConsoleColor.Magenta);

                                medicineService.GetMedicineById(searchId);
                                Helper.Print($"{searchId} medicines: ", ConsoleColor.Magenta);

                                foreach (var Medicine in DB.Medicines)
                                {
                                    if (Medicine.Id == searchId)
                                    {
                                        Console.WriteLine(Medicine);
                                    }

                                }

                            }
                            catch (NotFoundException ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);

                            }
                            catch (Exception ex)
                            {
                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);

                            }

                            break;
                        case "5"://name
                            try
                            {
                                Helper.Print("\n Enter the name: ", ConsoleColor.Magenta);

                                string searchName = Console.ReadLine();
                                Helper.Print($"{searchName} medicines: ", ConsoleColor.Magenta);

                                medicineService.GetMedicineByName(searchName);
                                foreach (var Medicine in DB.Medicines)
                                {
                                    if (Medicine.Name == searchName)
                                    {
                                        Console.WriteLine(Medicine);

                                    }
                                }
                            }
                            catch (NotFoundException ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);
                            }
                            catch (Exception ex)
                            {
                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);
                            }
                            break;
                        case "6":
                            try
                            {
                                decimal price;
                                Helper.Print("Enter the information of new medicine: ", ConsoleColor.Magenta);

                                Helper.Print("The name of medicine: ", ConsoleColor.Magenta);
                                string medicineName = Console.ReadLine();

                                Helper.Print("Price of medicine: ", ConsoleColor.Magenta);

                                price = decimal.Parse(Console.ReadLine());

                                Helper.Print("The categories of medicines: ", ConsoleColor.Green);
                                //Console.WriteLine("The categories of medicines: ");
                                foreach (var category in DB.Categories)
                                {
                                    Console.WriteLine(category);
                                }
                                Helper.Print("The id of category: ", ConsoleColor.Magenta);

                                int categoryId = int.Parse(Console.ReadLine());
                                Medicine newMedicine1 = new Medicine(medicineName, price, activeUser.Id, categoryId);
                                medicineService.CreateMedicine(newMedicine1);
                                Helper.Print("New medicine added succesfully.", ConsoleColor.Green);
                                //Console.WriteLine("New medicine added succesfully.");
                            }
                            catch (Exception ex)
                            {

                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);

                            }
                            break;

                        case "7"://remove
                            try
                            {
                                Helper.Print("\nEnter medicine ID to remove: ", ConsoleColor.Magenta);

                                int medicineIdToRemove = int.Parse(Console.ReadLine());
                                medicineService.RemoveMedicine(medicineIdToRemove, activeUser.Id);
                                Helper.Print($"Medicine id with {medicineIdToRemove} number removed", ConsoleColor.Green);
                                //Console.WriteLine($"{medicineIdToRemove} removed");
                            }
                            catch (NotFoundException ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);
                            }
                            catch (Exception ex)
                            {

                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);
                            }

                            break;
                        case "8"://update
                            try
                            {
                                Helper.Print("\nEnter medicine ID to update: ", ConsoleColor.Magenta);

                                int medicineUpdate = int.Parse(Console.ReadLine());

                                var existMedicine = medicineService.GetMedicineById(medicineUpdate);
                                Helper.Print("Enter updated medicine names: ", ConsoleColor.Magenta);
                                Helper.Print("New name: ", ConsoleColor.Magenta);

                                string updateMedicineName = Console.ReadLine();
                                Helper.Print("New  price: ", ConsoleColor.Magenta);

                                decimal updatedMedicinePrice = decimal.Parse(Console.ReadLine());
                                Helper.Print("New category ID: ", ConsoleColor.Magenta);

                                int updatedCategoryId = int.Parse(Console.ReadLine());
                                Medicine updatedMedicine = new Medicine(updateMedicineName, updatedMedicinePrice, activeUser.Id, updatedCategoryId);


                                medicineService.UpdateMedicine(medicineUpdate, updatedMedicine, activeUser.Id);
                                Console.WriteLine($"{existMedicine} updated");
                            }
                            catch (NotFoundException ex)
                            {
                                Helper.Print(ex.Message, ConsoleColor.Red);
                            }
                            catch (Exception ex)
                            {
                                Helper.Print($"Error: {ex.Message}", ConsoleColor.Red);
                            }
                            break;
                        case "9":
                            goto restart;
                            break;
                        case "10":
                            Helper.Print("All categories: ", ConsoleColor.Magenta);

                            foreach (var category in DB.Categories)
                            {
                                Console.WriteLine(category);
                            }
                            break;
                        default:
                            Helper.Print("FALSE CHOICE...Please, try again...", ConsoleColor.Red);
                            break;


                    }
                }
            }







































































            //Medicine medicine = new Medicine
            //{
            //    Name = "Parestamol",
            //    Price = 10,
            //    UserId = 1,
            //    CategoryId = 2,
            //    Id = 3,
            //};
            //UserService userService = new UserService();
            // MedicineService medicineService = new MedicineService();
            //User user1 = new User("Aysun Eminli","aem","123");
            //userService.AddUser(user1);


            // Console.WriteLine("Please, enter email and password: ");
            // Console.WriteLine("Email: ");
            // string email = Console.ReadLine();
            // Console.WriteLine("Password: ");
            // string password = Console.ReadLine();

            // try
            // {
            //     User loggedUser = userService.Login(email, password);
            //     Console.WriteLine("Succesfully accessed...");
            //     bool exit = false;
            //     while (!exit)
            //     {
            //         Console.WriteLine("Please, select the choice: ");
            //         Console.WriteLine("1.Add new medicine");
            //         Console.WriteLine("2.Show all medicines");
            //         Console.WriteLine("3.Show medicines for their categories");
            //         //id
            //         //name
            //         //create category
            //         Console.WriteLine("4.Remove medicine");
            //         Console.WriteLine("5.Update medicine");
            //         Console.WriteLine("6.Exit program");
            //         Console.WriteLine("Enter your choice: ");
            //         string choice = Console.ReadLine();

            //         switch (choice)
            //         {
            //             case "1":
            //                 Console.WriteLine("Enter the information of new medicine: ");
            //                 Console.WriteLine("The name of medicine: ");
            //                 string medicineName = Console.ReadLine();
            //                 Console.WriteLine("Price of medicine: ");
            //                 decimal price = int.Parse(Console.ReadLine());
            //                 Console.WriteLine("The id of category: ");
            //                 int categoryId = int.Parse(Console.ReadLine());
            //                 Medicine newMedicine1 = new Medicine
            //                 {
            //                     Name = medicineName,
            //                     Price = price,
            //                     CategoryId = categoryId,
            //                     UserId = user1.Id,
            //                     CreatedTime = DateTime.Now,

            //                 };
            //                 medicineService.CreateMedicine(newMedicine1);
            //                 Console.WriteLine("New medicine added succesfully.");
            //                 break;
            //             case "2":
            //                 Medicine[] allMedicines = medicineService.GetAllMedicines();
            //                 Console.WriteLine("\nAll medicines: ");
            //                 foreach (var Medicine in allMedicines)
            //                 {
            //                     Console.WriteLine($"ID: {Medicine.Id}, Name: {Medicine.Name}, Price: {Medicine.Price}, Category ID: {Medicine.CategoryId})");
            //                 }
            //                 break;
            //             case "3":
            //                 Console.WriteLine("\nEnter category id: ");
            //                 int categorySearchId = int.Parse(Console.ReadLine());
            //                 Medicine[] medicines = medicineService.GetAllMedicines();
            //                 Console.WriteLine($"category id {categorySearchId} medicines: ");
            //                 foreach (var Medicine in medicines)
            //                 {
            //                     Console.WriteLine(Medicine);
            //                 }

            //                 break;
            //             case "4":
            //                 Console.WriteLine("\nEnter medicine ID to remove: ");
            //                 int medicineIdRemove = int.Parse(Console.ReadLine());
            //                 medicineService.RemoveMedicine(medicineIdRemove);
            //                 Console.WriteLine($"{medicineIdRemove} removed");
            //                 break;
            //             case "5"://update
            //                 Console.WriteLine("\nEnter medicine ID to update: ");
            //                 int medicineUpdate = int.Parse(Console.ReadLine());

            //                 Console.WriteLine("Enter updated medicine names: ");
            //                 Console.WriteLine("Name: ");
            //                 string updateMedicineName = Console.ReadLine();
            //                 Console.WriteLine("Price: ");
            //                 decimal updatedMedicinePrice = decimal.Parse(Console.ReadLine());
            //                 Console.WriteLine("Category ID: ");
            //                 int updatedCategoryId = int.Parse(Console.ReadLine());
            //                 Medicine updatedMedicine = new Medicine
            //                 {
            //                     Id = medicineUpdate,
            //                     Name = updateMedicineName,
            //                     Price = updatedMedicinePrice,
            //                     CategoryId = updatedCategoryId,
            //                     UserId = medicineUpdate,
            //                     CreatedTime = DateTime.Now,

            //                 };
            //                 //SORUS
            //                 medicineService.CreateMedicine(updatedMedicine);
            //                 Console.WriteLine($"{updatedMedicine} updated");
            //                 break;
            //             case "6":
            //                 exit = true;
            //                 break;
            //             default:
            //                 Console.WriteLine("FALSE CHOICE...Please select a valid choice");
            //                 break;
            //         }
            //     }

            // }
            // catch(NotFoundException ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            // catch(Exception ex) {
            //     Console.WriteLine("An error occured: " +ex.Message);
            // }

        }
    }
}
