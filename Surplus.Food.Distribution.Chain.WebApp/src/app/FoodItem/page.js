"use client";
import React from "react";
import axios from "axios"; // Import axios for making HTTP requests
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";
import { useState, useEffect, useRef } from "react";
import "react-toastify/dist/ReactToastify.css";
import * as Yup from 'yup';

export default function FoodItem() {

    const validationSchema = Yup.object().shape({
        quantity: Yup.number().required('Quantity is required').min(1, 'Quantity must be greater than 0'),
        price: Yup.number().required('Price is required'),
    });

    const [foodcategory, setFoodcategory] = useState("");
    const [fooditems, setfoodItems] = useState([]);
    const [selectedimage, setSelectedImage] = useState(null);
    const [servicetype, setServiceTypeId] = useState(0);
    const [pricetatustype, setpriceStatus] = useState(0);
    const [itemId, setItemId] = useState("");
    const [pickupTime, setPickupTime] = useState("");
    const [location, setLocation] = useState("");
    const [offers, setOffers] = useState("");

    const fileInputRef = useRef(null);

    function fetchData() {
        const page = 1;

        axios.get(`${apiUrl}supplier/fooditem/${page}?userId=${localStorage.getItem('userId')}`)
            .then(response => {
                setfoodItems(response.data.foodItems);
            })
            .catch(error => {
                console.error(error);
            });
    }

    const handleFileChange = (event, setFieldValue) => {
        const file = event.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onloadend = () => {
                // The result contains the base64 representation of the image
                const base64String = reader.result;

                // Convert the base64 string to a byte array
                const byteCharacters = atob(base64String.split(',')[1]);
                const byteArray = new Uint8Array(byteCharacters.length);

                for (let i = 0; i < byteCharacters.length; i++) {
                    byteArray[i] = byteCharacters.charCodeAt(i);
                }

                setFieldValue("image", base64String);
            };

            reader.readAsDataURL(file);
        }
    };

    const getImageSource = (imageData) => {
        if (imageData && Array.isArray(imageData)) {
            const base64String = btoa(String.fromCharCode(...imageData));
            return `data:image/png;base64,${base64String}`;
        }
        return `data:image/png;base64,${imageData}`;
    };

    useEffect(() => {
        console.log('Current state:', foodcategory, servicetype, pricetatustype, selectedimage);
        fetchData();
    }, [foodcategory, servicetype, pricetatustype, selectedimage]);

    return (
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h1 class="text-yellow-950 mb-5">Welcome back {localStorage.getItem('supplierName')}</h1>
                <h2 class="text-yellow-950 mb-5">Food Items</h2>
                <hr></hr>
            </div>

            <div class="grid grid-flow-col grid-cols-6">
                <div class="col-span-2">
                    <Formik
                        initialValues={{
                            category: foodcategory,
                            foodSupplierId: localStorage.getItem('supplierId'),
                            image: selectedimage,
                            quantity: 0,
                            price: 0,
                            serviceTypeId: servicetype,
                            priceStatus: pricetatustype,
                            location: location,
                            pickupTime: pickupTime,
                            offers: offers
                        }}
                        validationSchema={validationSchema}
                        onSubmit={(values, { setSubmitting, resetForm }) => {
                            // Make a POST request to your backend API
                            console.log(values);
                            axios
                                .post(`${apiUrl}supplier/fooditem`, values)
                                .then((response) => {
                                    console.log(response.data);
                                    toast.success("Food item added successfully!");
                                    fetchData();
                                    resetForm();

                                    if (fileInputRef.current) {
                                        fileInputRef.current.value = '';
                                    }
                                })
                                .catch((error) => {
                                    // Handle errors
                                    console.error("Adding food item error:", error);
                                    toast.error("Adding food item failed. Please try again.");
                                })
                                .finally(() => {
                                    setSubmitting(false);
                                });
                        }}
                    >
                        {({ isSubmitting, values, handleChange, setFieldValue }) => (
                            <Form>
                                <div className="grid grid-cols-3 gap-2 mt-5">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Food Category <span className="text-red-700">*</span></label>
                                        <select required id="category" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.category}>
                                            <option value="">Select a Category</option>
                                            <option value="Rice and Curry">Rice and Curry</option>
                                            <option value="Fried Rice">Fried Rice</option>
                                            <option value="Noodles">Noodles</option>
                                            <option value="String Hoppers">String Hoppers</option>
                                            <option value="Pastry Items">Pastry Items</option>
                                            <option value="Curries">Curries</option>
                                            <option value="Drinks">Drinks</option>
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Quantity <span className="text-red-700">*</span></label>
                                        <Field required type="text" name="quantity" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="quantity" component="div" className="text-red-500 text-xs italic" />
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Price <span className="text-red-700">*</span></label>
                                        <Field required type="text" name="price" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="price" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>

                                <div className="grid grid-cols-3 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Delivery <span className="text-red-700">*</span></label>
                                        <select
                                            required
                                            id="serviceTypeId"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.serviceTypeId}
                                        >
                                            <option value="">Select a Delivery</option>
                                            <option value='1'>Delivery</option>
                                            <option value="2">Self Pickup</option>
                                            <option value="3">Delivery Free</option>
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Food Status <span className="text-red-700">*</span></label>
                                        <select
                                            required
                                            id="priceStatus"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.priceStatus}
                                        >
                                            <option value="">Select a Food Status</option>
                                            <option value="1">Free</option>
                                            <option value="2">Discounted</option>
                                            {/* priceStatus */}
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Offers Available <span className="text-red-700">*</span></label>
                                        <select required id="offers" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.offers}
                                        >
                                            <option value="">Select an Offer</option>
                                            <option value="Yes">Yes</option>
                                            <option value="No">No</option>
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>

                                <div className="grid grid-cols-3 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2" >Pickup Time <span className="text-red-700">*</span></label>
                                        <select
                                            required
                                            id="pickupTime"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.pickupTime}
                                        >
                                            <option value="">Select a Pickup Time</option>
                                            <option value="8-10 am">8-10 am</option>
                                            <option value="10-12 am">10-12 pm</option>
                                            <option value="12-2 pm">12-2 pm</option>
                                            <option value="2-6 pm">2-6 pm</option>
                                            <option value="6-10 pm">6-10 pm</option>
                                        </select>
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Location <span className="text-red-700">*</span></label>
                                        <select
                                            required
                                            id="location"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.location}
                                        >
                                            <option value="">Select a Location</option>
                                            <option value="Colombo 1">Colombo 1</option>
                                            <option value="Colombo 2">Colombo 2</option>
                                            <option value="Colombo 3">Colombo 3</option>
                                        </select>
                                    </div>

                                    <div className="mb-4">
                                        <label className="block text-gray-700 text-sm font-bold mb-2" for="file_input">Item Image</label>
                                        <input ref={fileInputRef} className="shadow appearance-none w-full text-gray-700 border rounded py-2 px-4 leading-tight focus:outline-none cursor-pointer focus:outline-none focus:shadow-outline" id="file_input" type="file"
                                            onChange={(event) => handleFileChange(event, setFieldValue)}
                                        />
                                        <p class="mt-1 text-sm text-gray-500 dark:text-gray-300" id="file_input_help">SVG, PNG, JPG or GIF (MAX. 800x400px).</p>
                                        <ErrorMessage name="contactNo" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>
                                <div className="flex items-center justify-between">
                                    <button type="submit" disabled={isSubmitting} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2">
                                        {isSubmitting ? 'Adding...' : 'Add Food Item'}
                                    </button>
                                </div>
                            </Form>
                        )}
                    </Formik>
                    <ToastContainer />
                </div>

                <div class="ml-3 col-span-4">
                    <table className="table-auto w-full border border-gray-300 mt-5">
                        <thead>
                            <tr className="bg-gray-200 text-left">
                                <th className="px-4 py-2">Food Category</th>
                                <th className="px-4 py-2">Quantity</th>
                                <th className="px-4 py-2">Price</th>
                                <th className="px-4 py-2">Image</th>
                                <th className="px-4 py-2">Price Status</th>
                                <th className="px-4 py-2">Service Type</th>
                            </tr>
                        </thead>
                        <tbody>
                            {fooditems.map((fooditem) => (
                                <tr key={fooditem.id} className="text-gray-700 hover:bg-gray-100">
                                    <td className="px-4 py-2">{fooditem.category}</td>
                                    <td className="px-4 py-2">{fooditem.quantity}</td>
                                    <td className="px-4 py-2">{fooditem.price}</td>
                                    <td className="px-4 py-2">
                                        <img src={getImageSource(fooditem.image)} alt="fooditem" className="w-20 h-20" />
                                    </td>
                                    <td className="px-4 py-2">{fooditem.priceStatus}</td>
                                    <td className="px-4 py-2">{fooditem.serviceTypeId}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}