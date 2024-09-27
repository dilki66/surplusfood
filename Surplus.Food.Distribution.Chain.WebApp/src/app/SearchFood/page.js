"use client";

import React, { useEffect, useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";

import styles from "./SearchFood.module.css";
import axios from "axios";
import FoodCard from "../components/FoodCard/page";
import apiUrl from "../config/apiurl";

export default function SearchFood() {
  const [suupliers, setSuppliers] = useState([]);
  const [fooddata, setfoodData] = useState([]);

  console.log({fooddata})

  useEffect(() => {
    axios
      .get("https://localhost:7044/api/supplier/supplier")
      .then((response) => {
        console.log(response.data);
        setSuppliers(response.data);
      });
  }, []);

  const foodData = [
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    // ... more food items
  ];

  console.log({ suupliers });

  return (
    // ms-64
    <div className=" ms-80 mt-20 mr-20">
      <div class="grid-col text-3xl font-bold">
        <h1 class="text-yellow-950 mb-5"> Search Food</h1>
        <hr></hr>
      </div>

      <Formik
        initialValues={{
          foodSupplier: suupliers.length > 0 ? suupliers[0].id : "",
          foodCategory: "Rice and Curry",
          offers: "Yes",
          deliveryStatus: "2",
          pickupTime: "8-10 am",
          foodStatus: "1",
          location: "Colombo 1",
          
        }}
        validationSchema={Yup.object({})}
        onSubmit={(values) => {
          
          console.log({values})
          axios
              .get(`${apiUrl}customer/food/1?supplierId=${values.foodSupplier}&foodCategory=${values.foodCategory}&foodStatus=${values.foodStatus}&location=${values.location}&deliveryStatus=${values.deliveryStatus}&pickupTime=${values.pickupTime}&offers=${values.offers}`)
              .then((response) => {
                 console.log({response})
                 setfoodData(response.data.foodItems)
              })
              
        }}
        enableReinitialize={true}
      >
        {({ values, handleChange }) => (
          <Form>
            <div class="grid grid-cols-3 mt-5"></div>
            <div class="grid grid-cols-3">
              <div>
                <div>
                  <div className="mt w-80">
                    <label
                      htmlFor="text"
                      className="block text-gray-700 text-sm font-bold mb-2"
                    >
                      Food Suppliers
                    </label>

                    <select
                      id="foodSupplier"
                      className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                      onChange={handleChange}
                      value={values.foodSupplier}
                    >
                      {suupliers?.map((supplier) => (
                        <option value={supplier?.id} key={supplier.id}>
                          {supplier?.suplierName}
                        </option>
                      ))}
                    </select>
                    <ErrorMessage
                      name="foodCategory"
                      component="div"
                      className="text-red-500 text-xs italic"
                    />
                  </div>

                  <div className="mb-4 mt-10 w-80">
                    <label
                      htmlFor="text"
                      className="block text-gray-700 text-sm font-bold mb-2"
                    >
                      Food Category
                    </label>
                    <select
                      id="foodCategory"
                      className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                      onChange={handleChange}
                      value={values.foodCategory}
                    >
                      <option value="Rice and Curry">Rice and Curry</option>
                      <option value="Fried Rice">Fried Rice</option>
                      <option value="Noodles">Noodles</option>
                      <option value="String Hoppers">String Hoppers</option>
                      <option value="Pastry Items">Pastry Items</option>
                      <option value="Curries">Curries</option>
                      <option value="Drinks">Drinks</option>
                    </select>
                    <ErrorMessage
                      name="foodCategory"
                      component="div"
                      className="text-red-500 text-xs italic"
                    />
                  </div>

                  <div className="mb-4 mt-10 w-80">
                    <label
                      htmlFor="offers"
                      className="block text-gray-700 text-sm font-bold mb-2"
                    >
                      Offers
                    </label>
                    <select
                      id="offers"
                      className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                      onChange={handleChange}
                      value={values.offers}
                    >
                      <option value="Yes">Yes</option>
                      <option value="No">No</option>
                    </select>
                    <ErrorMessage
                      name="offers"
                      component="div"
                      className="text-red-500 text-xs italic"
                    />
                  </div>
                </div>

                <div className="mb-4 w-80 mt-10">
                  <label
                    htmlFor="text"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Delivery Status
                  </label>

                  <div className="flex items-center mb-4">
                    <Field
                      //id="default-radio-1"
                      type="radio"
                      value="2"
                      name="deliveryStatus"
                      className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                    />
                    <label
                      htmlFor="default-radio-1"
                      className="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                    >
                      Self Pickup
                    </label>
                  </div>
                  <div className="flex items-center mb-4">
                    <Field
                      //id="default-radio-2"
                      type="radio"
                      value="1"
                      name="deliveryStatus"
                      className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                    />
                    <label
                      htmlFor="default-radio-2"
                      className="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                    >
                      Delivery
                    </label>
                  </div>

                  <div className="flex items-center mb-4">
                    <Field
                      //id="default-radio-3"
                      type="radio"
                      value="3"
                      name="deliveryStatus"
                      className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                    />
                    <label
                      htmlFor="default-radio-3"
                      className="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                    >
                      Free Delivery
                    </label>
                  </div>
                </div>
              </div>

              <div className="grid-cols-4">
                <div className="mb-4 w-80">
                  <label
                    htmlFor="text"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Pickup Time
                  </label>
                  <select
                    id="pickupTime"
                    class="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    onChange={handleChange}
                    value={values.pickupTime}
                  >
                    <option value="8-10 am">8-10 am</option>
                    <option value="10-12 am">10-12 am</option>
                    <option value="12-2 pm">12-2 pm</option>
                    <option value="2-6 pm">2-6 pm</option>
                    <option value="6-10 pm">6-10 pm</option>
                  </select>

                  <ErrorMessage
                    name="email"
                    component="div"
                    className="text-red-500 text-xs italic"
                  />
                </div>

                <div className="mb-4 mt-10 w-80">
                  <label
                    htmlFor="text"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Food Status
                  </label>
                  <select
                    id="foodStatus"
                    class="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    onChange={handleChange}
                    value={values.foodStatus}
                  >
                    <option value="1">Free</option>
                    <option value="2">Discounted</option>
                  </select>

                  <ErrorMessage
                    name="email"
                    component="div"
                    className="text-red-500 text-xs italic"
                  />
                </div>

                <div className="mb-4 mt-10 w-80">
                  <label
                    htmlFor="text"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Location
                  </label>
                  <select
                    id="location"
                    class="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    onChange={handleChange}
                    value={values.location}
                  >
                    <option value="Colombo 1">Colombo 1</option>
                    <option value="Colombo 2">Colombo 2</option>
                    <option value="Colombo 3">Colombo 3</option>
                  </select>

                  <ErrorMessage
                    name="email"
                    component="div"
                    className="text-red-500 text-xs italic"
                  />
                </div>
                <div className="mb-4 mt-10 w-80">
                  <button
                    type="submit"
                    class="text-white bg-yellow-950 hover:bg-yellow-950 focus:outline-none focus:ring-4 focus:ring-yellow-300 font-medium rounded-full text-sm px-5 py-2.5 text-center mb-2 "
                  >
                    Search
                  </button>
                </div>
              </div>

              <div className="grid grid-cols-3 mt-10">
                <div className={styles.foodImage}></div>
              </div>
            </div>
          </Form>
        )}
      </Formik>


      <div>
      <div className=" mt-20">
      <div className="grid-col text-3xl font-bold">
        <h1 className="text-yellow-950 mb-5">Search Results</h1>
        <hr />
      </div>

      <div className="mt-10 grid grid-cols-2 gap-4"> {/* Using grid for columns */}
        {fooddata?.map((food) => (
          <FoodCard key={food.name} {...food} />
        ))}
      </div>
    </div>
      </div>

    </div>
  );
}
