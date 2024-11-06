'use client';

import "react-toastify/dist/ReactToastify.css";
import React, { useEffect, useState } from 'react';
import FoodCardCart from '../components/FoodCardCart/page';
import axios from 'axios';
import apiUrl from '../config/apiurl';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify';



export default function Cart() {

    var cusAddress = ""

    if (typeof window !== "undefined") {
        cusAddress = localStorage.getItem('address');
    } else {
        console.log("localStorage is not available in this context.");
    }


    const [foodData, setFoodData] = useState([]);
    const [total, setTotal] = useState([]);

    const [priceStatusId, setPriceStatusId] = useState(0);

    const [servicetype, setServiceTypeId] = useState(1);

    const [checkout, setCheckout] = useState('Place Order');

    const [pickupTime, setPickupTime] = useState('8-10 am');

    const [paymentMeth, setPaymentMeth] = useState("N/A");

    const [suppName, setSuppName] = useState("Not Available");


    const [editableLocation, setEditableLocation] = useState(cusAddress);

    const handleLocation = (event, setFieldValue) => {
        const selectedLocation = event.target.value;
        setEditableLocation(selectedLocation);
        setFieldValue("location", selectedLocation);
    }

    const handleServiceType = (event, setFieldValue) => {
        const selectedServiceType = event.target.value;
        setServiceTypeId(selectedServiceType);
        setFieldValue("serviceTypeId", selectedServiceType);
    };

    const handleTime = (event, setFieldValue) => {
        const selectedTime = event.target.value;
        setPickupTime(selectedTime);
        setFieldValue("pickupTime", selectedTime);
    };

    const handleCheckout = (event, setFieldValue) => {
        const selectedCheckout = event.target.value;
        setCheckout(selectedCheckout);
        setFieldValue("checkout", selectedCheckout);
    }


    const fetchData = () => {
        const userId = localStorage.getItem('userId');

        axios.get(`${apiUrl}order/cart/${userId}`)
            .then(response => {
                setFoodData(response.data.foodList);
                setSuppName(response.data.supplierName);
                console.log(response.data.supplierName);
                if (servicetype === 1) {
                    setTotal(response.data.total);
                }
                else {
                    setTotal(response.data.total);
                }

                console.log(response.data.foodList);

                if (response.data.total > 0) {
                    setPriceStatusId(2);
                }
                else {
                    setPriceStatusId(1);
                }
            })
            .catch(error => {
                console.error(error);
            });
    }


    useEffect(() => {

        const token = localStorage.getItem("token");

        if (!token) {
            console.log("token", token);
            window.location.href = "/login";
        }

        fetchData();



    }, []);




    return (
        <div className="ms-80 mt-20 mr-20">
            <div className="grid-col">
                <h1 className="text-yellow-950 mb-5 text-3xl font-bold">Cart</h1>
                <h3 className="text-yellow-950 mb-2 font-bold">Supplier: {suppName}</h3>
                <hr />
            </div>

            <div className="mt-10 grid grid-cols-2 gap-4">
                {foodData.map((food) => (
                    <FoodCardCart key={food.foodItemId} {...food} foods={food} />
                ))}
            </div>

            <div className="mt-10 w-auto bg-orange-100 p-4 mb-8 rounded flex justify-between">
                <p className='text-3xl font-bold'>
                    Total
                </p>
                <p className='text-2xl font-medium'>
                    {total} LKR
                </p>
            </div>

            <Formik initialValues={{ location: editableLocation, serviceTypeId: "1", pickupTime: "8-10 am", recieverName: "", foodItems: foodData, }}
                onSubmit={(values, { setSubmitting, resetForm }) => {
                    // Map the foodData array to the required structure
                    const foodItems = foodData.map(food => ({
                        id: food.foodItemId,
                        quantity: food.quantity,
                        amount: food.amount
                    }));

                    if (priceStatusId !== 1 && servicetype === 1 || servicetype === 3) {
                        setPaymentMeth("Card");
                    }

                    // Make a POST request to your backend API
                    axios
                        .post(`${apiUrl}order/food`, {
                            userId: localStorage.getItem('userId'),
                            recieverName: values.recieverName,
                            serviceTypeId: parseInt(values.serviceTypeId),
                            location: values.location ?? editableLocation,
                            priceStatusId: priceStatusId,
                            pickupTime: values.pickupTime,
                            foodItems: foodItems,
                            paymentMethod: paymentMeth
                        })
                        .then(() => {

                            if (checkout === "Self Checkout") {

                                console.log("priceStatusId", priceStatusId);
                                console.log("servicetype", servicetype);

                                if (priceStatusId === 1 && (servicetype === 3 || servicetype === "3")) {
                                    toast.success("Checkout Successful!");
                                    window.location.href = "/";
                                }

                                else if (priceStatusId === 1 && (servicetype === 2 || servicetype === "2")) {
                                    toast.success("Checkout Successful!");
                                    window.location.href = "/";
                                }
                                else {
                                    toast.success("Checkout Successful!");
                                    window.location.href = "/Payment";
                                }
                            }
                            else if (checkout === "Place Order") {
                                window.location.href = "/";
                                console.log("priceStatusId", priceStatusId);
                                console.log("servicetype", servicetype);
                            }

                            resetForm();
                        })
                        .catch((error) => {
                            // Handle errors
                            toast.error("Checkout failed. Please try again."); // Display error message using toastify
                            console.error("Checkout error:", error);
                        })
                        .finally(() => {
                            setSubmitting(false); // Reset submitting state
                        });
                }}

            >

                {({ isSubmitting, setFieldValue }) => (

                    <Form>


                        <div className="flex space-5">

                            <div className="mb-4 mr-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Delivery</label>
                                <select
                                    id="servicetype"
                                    className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    onChange={(event) => handleServiceType(event, setFieldValue)}
                                    value={servicetype}
                                >
                                    <option value='1'>Delivery</option>
                                    <option value="2">Self Pickup</option>
                                    <option value="3">Delivery Free</option>
                                </select>
                                <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                            </div>

                            <div className="mb-6 mr-4">
                                <label htmlFor="location" className="block text-gray-700 text-sm font-bold mb-2">Receiver Address</label>
                                <Field value={editableLocation}
                                    onChange={(event) => handleLocation(event, setFieldValue)}
                                    required type="text" name="location" className=" shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="location" component="div" className="text-red-500 text-xs italic" />
                            </div>

                            <div className="mb-4 mr-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Checkout Type</label>
                                <select id="countries" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    onChange={(event) => handleCheckout(event, setFieldValue)}
                                    value={checkout}
                                >
                                    <option selected value="Self Checkout">Self Checkout</option>
                                    <option value="Place Order">Place Order</option>
                                </select>
                                <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                            </div>

                            <div className="mb-4 mr-4">
                                <label
                                    htmlFor="text"
                                    className="block text-gray-700 text-sm font-bold mb-2"
                                >
                                    Pickup Time
                                </label>
                                <select
                                    id="pickupTime"
                                    class="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    onChange={(event) => handleTime(event, setFieldValue)}
                                    value={pickupTime}
                                >
                                    <option selected value="8-10 am">8-10 am</option>
                                    <option value="10-12 pm">10-12 pm</option>
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

                            <div className="mb-6">
                                <label htmlFor="recieverName" className="block text-gray-700 text-sm font-bold mb-2"> Reciever Name</label>
                                <Field required type="text" name="recieverName" className=" shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="recieverName" component="div" className="text-red-500 text-xs italic" />
                            </div>
                        </div>




                        <div className="w-auto mb-20   flex justify-end">
                            <button disabled={isSubmitting} className="bg-yellow-900 text-lg text-white font-medium py-4 px-10 rounded hover:bg-yellow-950">
                                Checkout
                            </button>
                        </div>
                    </Form>

                )}

            </Formik>


            <ToastContainer /> {/* Toast container */}
        </div>
    );
}
