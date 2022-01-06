﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcCustomersService;

namespace Pascu_Serban_Lab1.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public CustomersGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            Customer customer = client.Get(new CustomerId() { Id = (int)id });
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);
                Customer response = client.Update(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new CustomerService.CustomerServiceClient(channel);
            Customer customer = client.Get(new CustomerId() { Id = (int)id });

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);

            //Customer customer = client.Get(new CustomerId() {Id = (int) id});
            var customerId = new CustomerId() { Id = (int)id };

            client.Delete(customerId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            Empty response = client.Delete(new CustomerId()
            {
                Id = id
            });
            return RedirectToAction(nameof(Index));
        }
    }
}