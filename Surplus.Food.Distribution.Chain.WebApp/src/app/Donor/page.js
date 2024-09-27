'use client';
import axios from "axios"; // Import axios for making HTTP requests
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";
import React, { useState, useEffect } from 'react';
import "react-toastify/dist/ReactToastify.css";


export default function Donor() {
    const [orders, setOrders] = useState([]);
    const [orderId, setOrderId] = useState("");
    const [amount, setAmount] = useState(0);

    const [paymentType, setPaymentType] = useState("Card");

    const handlePaymentType = (event, setFieldValue) => {
        setPaymentType(event.target.value);
        setFieldValue('paymentType', event.target.value);
    }

    var userRole = ""
    var userId = ""

    if (typeof window !== "undefined") {
        userRole = localStorage.getItem('role');
        userId = localStorage.getItem('userId');
    } else {
        console.log("localStorage is not available in this context.");
    }

    function fetchData() {
        const page = 1;

        if (userRole === "Guest") {
            axios.get(`${apiUrl}order/donor/${page}`)
                .then(response => {
                    setOrders(response.data.orders);
                    console.log(response.data.orders);
                })
                .catch(error => {
                    console.error(error);
                });
        }
        else if (userRole === "Customer") {
            axios.get(`${apiUrl}order/latest/${userId}`)
                .then(response => {
                    setOrders(response.data.orders);
                    console.log(response.data.orders);
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }

    const pay = (orderId, amount) => {
        setOrderId(orderId);
        setAmount(amount);
    }


    useEffect(() => {
        fetchData();
    }, []);

    return (
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h1 class="text-yellow-950 mb-5">Donor</h1>
                <hr></hr>
            </div>

            <div class="grid grid-flow-col grid-cols-6">
                <div class="col-span-3">

                    <table className="table-auto w-full border border-gray-300 mt-20">
                        <thead>
                            <tr className="bg-gray-200 text-left">
                                <th className="px-4 py-2">Order Id</th>
                                <th className="px-4 py-2">Price</th>
                                <th className="px-4 py-2">Pay</th>
                            </tr>
                        </thead>
                        {orders.map((order) => (
                            order.orderStatusId !== 'Completed' && (
                                <tr key={order.id} className="text-gray-700 hover:bg-gray-100">
                                    <td className="px-4 py-2">{order.id}</td>
                                    <td className="px-4 py-2">{order.price}</td>
                                    <td className="px-4 py-2">
                                        <button
                                            class="bg-amber-400 hover:bg-amber-600 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => pay(order.id, order.price)}
                                        >
                                            Proceed
                                        </button>
                                    </td>
                                </tr>
                            )
                        ))}
                    </table>
                </div>

                <div class="ml-3 col-span-3">
                    <Formik
                        initialValues={{ donerName: "", contactNo: "", paymentType: paymentType, cardholderName: "", cardNo: "", expireDate: "", securityCode: "", amount: "" }}

                        onSubmit={(values, { setSubmitting }) => {
                            axios.post(`${apiUrl}donor/payment?orderId=${orderId}`, values)
                                .then(response => {
                                    console.log('Payment success:', response.data);
                                    setSubmitting(false);
                                    toast.success("Payment success!");

                                    window.location.reload();
                                })
                                .catch(error => {
                                    console.error('Payment error:', error);
                                    toast.error("Payment failed!");
                                });
                        }}
                    >
                        {({ isSubmitting, setFieldValue }) => (
                            <Form>
                                <div className="grid grid-cols-5 mt-20 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Order Id</label>
                                        <Field type="text" value={orderId} name="orderId" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="orderId" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Ammount</label>
                                        <Field type="text" name="amount" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="amount" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Donor Name</label>
                                        <Field type="text" name="donerName" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="donerName" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Contact No</label>
                                        <Field type="text" name="contactNo" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="contactNo" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    {/* <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Payment Type</label>
                                        <Field type="text" name="paymentType" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="paymentType" component="div" className="text-red-500 text-xs italic" />
                                    </div> */}

                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Payment Type</label>
                                        <select
                                            id="paymentType"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            onChange={(event) => handlePaymentType(event, setFieldValue)}
                                            value={paymentType}
                                        >
                                            <option selected value='Card'>Card</option>
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>

                                <div className="grid grid-cols-4 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="nic" className="block text-gray-700 text-sm font-bold mb-2">Cardholder Name</label>
                                        <Field type="text" name="cardholderName" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="cardholderName" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-4">
                                        <label htmlFor="nic" className="block text-gray-700 text-sm font-bold mb-2">Card Number</label>
                                        <Field type="text" name="cardNo" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="cardNo" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Expire Date</label>
                                        <Field type="text" placeholder="MM/YY" name="expireDate" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="expireDate" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                    <div className="mb-6-">
                                        <label htmlFor="password" className="block text-gray-700 text-sm font-bold mb-2">CVV</label>
                                        <Field type="password" name="securityCode" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                        <ErrorMessage name="securityCode" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>
                                <div className="grid grid-cols-4 gap-2">
                                    <div className="mb-4">
                                        <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Country</label>
                                        <select
                                            id="servicetype"
                                            className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        >
                                            <option value='1'>Sri Lanka</option>
                                            <option value="2">India</option>
                                        </select>
                                        <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                                    </div>
                                </div>

                                <div className="flex items-center justify-between">
                                    <button type="submit" disabled={isSubmitting} className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2">
                                        {isSubmitting ? 'Paying...' : 'Pay'}
                                    </button>
                                </div>
                            </Form>
                        )}
                    </Formik>
                    <ToastContainer />
                </div>

            </div>
        </div>
    );
}