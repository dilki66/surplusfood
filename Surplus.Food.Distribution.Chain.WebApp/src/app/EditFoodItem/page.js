    "use client";
import React from "react";
import axios from "axios"; // Import axios for making HTTP requests
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";
import { useState, useEffect, useRef } from "react";
import "react-toastify/dist/ReactToastify.css";
import * as Yup from 'yup';

export default function EditFoodItem() {

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
    const [pickupTime, setPickupTime] = useState("8-10 am");
    const [location, setLocation] = useState("Colombo 1");
    const [offers, setOffers] = useState("");
    const [quantity, setQty] = useState(0);
    const [price, setPrice] = useState(0);

    const [selectedFoodItem, setSelectedFoodItem] = useState(null);
    const fileInputRef = useRef(null);


    const handleItemId = (item) => {
        setSelectedFoodItem(item);

        setItemId(item.id);
        setFoodcategory(item.category);
        setQty(item.quantity);
        setPrice(item.price);

        if (item.serviceTypeId === "Delivery") {
            setServiceTypeId(1);
        }
        if (item.serviceTypeId === "Self Pickup") {
            setServiceTypeId(2);
        }
        if (item.serviceTypeId === "Delivery Free") {
            setServiceTypeId(3);
        }

        if (item.priceStatus === "Free") {
            setpriceStatus(1);
        }
        else {
            setpriceStatus(2);
        }

        setOffers(item.offers);
    };

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

    function deleteFoodItem(itemId) {
        axios.delete(`${apiUrl}supplier/fooditem/${itemId}`)
            .then(response => {
                console.log('Food item deleted:', response.data);
                toast.success("Delete success!");
                fetchData();
            })
            .catch(error => {
                console.error('Error deleting food item:', error);
                toast.error("Delete failed. Please try again.");
            });
    }

    const getImageSource = (imageData) => {
        if (imageData && Array.isArray(imageData)) {
            const base64String = btoa(String.fromCharCode(...imageData));
            return `data:image/png;base64,${base64String}`;
        }
        return `data:image/png;base64,${imageData}`;
    };

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

    function handleRest() {
        setItemId("");
        setFoodcategory("");
        setQty(0);
        setPrice(0);
        setServiceTypeId(0);
        setpriceStatus(0);
        setOffers("");

        if (fileInputRef.current) {
            fileInputRef.current.value = '';
        }
    }

    useEffect(() => {
        console.log('Current state:', foodcategory, servicetype, pricetatustype, selectedimage);
        fetchData();

        if (selectedFoodItem) {
            setSelectedFoodItem(null);
        }
    }, [foodcategory, servicetype, pricetatustype, selectedimage, selectedFoodItem]);

    return (
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h1 class="text-yellow-950 mb-5">Welcome back {localStorage.getItem('supplierName')}</h1>
                <h2 class="text-yellow-950 mb-5">Edit Food Items</h2>
                <hr></hr>
            </div>


            <div class="grid grid-flow-col grid-cols-6">
                <div class="col-span-2">
                    <Formik
                        initialValues={{
                            category: foodcategory,
                            foodSupplierId: localStorage.getItem('supplierId'),
                            image: selectedimage,
                            quantity: quantity ?? 0,
                            price: price ?? 0,
                            serviceTypeId: servicetype,
                            priceStatus: pricetatustype,
                            offers: offers,
                        }}
                        validationSchema={validationSchema}
                        onSubmit={(values, { setSubmitting, resetForm }) => {
                            // Make a POST request to your backend API
                            console.log(values);
                            axios
                                .put(`${apiUrl}supplier/fooditem/${itemId}`, values)
                                .then((response) => {
                                    // Handle successful login
                                    console.log(response.data); // You can access response data here
                                    toast.success("Food item updated successfully!");
                                    resetForm();
                                    handleRest();
                                    fetchData();
                                })
                                .catch((error) => {
                                    // Handle errors
                                    console.error("Adding food item error:", error);
                                    toast.error("Adding food item failed. Please try again."); // Display error message using toastify
                                })
                                .finally(() => {
                                    setSubmitting(false); // Reset submitting state
                                });
                        }}
                        enableReinitialize={true}
                    >
                        {({ isSubmitting, values, handleChange, setFieldValue }) => (
                            <Form>
                                <div className="grid grid-cols-3 gap-2 mt-7">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Id <span className="text-red-700">*</span></label>
                                        <Field type="text" value={itemId} name="id" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="id" component="div" className="text-red-500 text-xs italic" />
                                    </div>

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
                                </div>

                                <div className="grid grid-cols-3 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Price <span className="text-red-700">*</span></label>
                                        <Field required type="text" name="price" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="price" component="div" className="text-red-500 text-xs italic" />
                                    </div>

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Delivery <span className="text-red-700">*</span></label>
                                        <select
                                            required
                                            id="serviceTypeId"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={handleChange}
                                            value={values.serviceTypeId}
                                        >
                                            <option value="0">Select a Delivery</option>
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
                                            <option value="0">Select a Food Status</option>
                                            <option value="1">Free</option>
                                            <option value="2">Discounted</option>
                                            {/* priceStatus */}
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>

                                <div className="grid grid-cols-3 gap-2">
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
                                        {isSubmitting ? 'Updating...' : 'Update Food Item'}
                                    </button>
                                </div>
                            </Form>
                        )}
                    </Formik>
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
                                <th className="px-4 py-2">Update Item</th>
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
                                    <td className="px-4 py-2">
                                        <button class="bg-amber-400 hover:bg-amber-600 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => handleItemId(fooditem)}
                                        >
                                            Edit
                                        </button>
                                        <button class="bg-red-600 hover:bg-red-800 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2"
                                            onClick={() => deleteFoodItem(fooditem.id)}
                                        >
                                            Delete
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}