"use client";
import apiUrl from "@/app/config/apiurl";
import axios from "axios";
import React, { useState, useEffect } from 'react';
import "react-toastify/dist/ReactToastify.css";
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify

export default function SupplierOrderList() {
    const [orders, setOrders] = useState([]);
    const [orderStatus, setOrderStatus] = useState('');
    var userRole = ""
    var userId = ""

    if (typeof window !== "undefined") {
        userRole = localStorage.getItem('role');
        userId = localStorage.getItem("userId");
    } else {
        console.log("localStorage is not available in this context.");
    }

    function fetchData() {
        const page = 1;

        axios.get(`${apiUrl}order/supplier/${userId}/${page}`)
            .then(response => {
                setOrders(response.data.orders);
                console.log(response.data.orders);
            })
            .catch(error => {
                console.error(error);
            });
    }

    function updateOrderStatus(orderId, statusId) {
        axios.put(`${apiUrl}order/${orderId}/${statusId}`)
            .then(response => {
                console.log('Order status updated:', response.data);
                toast.success("Order status updated success!");
                fetchData();
            })
            .catch(error => {
                console.error('Error updating order status:', error);
                toast.error("Order status update failed!");
            });
    }

    const handleOrderStatusChange = (event) => {
        setOrderStatus(event.target.value);
    }

    const filteredOrders = orders.filter((order) => {
        console.log('order.orderStatusId:', order.orderStatusId);
        console.log('selectedOrderStatus:', orderStatus);

        if (orderStatus === 'Pending') {
            return order.orderStatusId === 'Pending' || order.orderStatusId === 'Paid';
        }

        return orderStatus === '' || order.orderStatusId === orderStatus;
    });

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <div className=" ms-80 mt-20 mr-20">
            <div className="grid-col text-3xl font-bold">
                <h1 className="text-yellow-950 mb-5"> Order List</h1>
                <hr></hr>
            </div>

            <select
                class="shadow appearance-none border rounded py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mt-10 p-2"
                value={orderStatus}
                onChange={handleOrderStatusChange}
            >
                {/* <option value="">All Status</option>
                {Array.from(new Set(orders.map((order) => order.orderStatusId))).map((location) => (
                    <option key={location} value={location}>
                        {location}
                    </option>
                ))} */}
                <option selected value="">All Status</option>
                <option value="Pending">New Orders</option>
                <option value="Accepted">In Process</option>
                <option value="Rejected">Rejected</option>
                <option value="Completed">Process Complete</option>
                <option value="Pending Delivery Acceptance">Pending Delivery Acceptance</option>
                <option value="Delivery Accepted">Pick up</option>
                <option value="On the way">On the way</option>
                <option value="Recieved">Customer recieved</option>
                <option value="Done">Done</option>
            </select>

            <table className="table-auto w-full border border-gray-300 mt-5">
                <thead>
                    <tr className="bg-gray-200 text-left">
                        <th className="px-4 py-2">Id</th>
                        <th className="px-4 py-2">Pickup Time</th>
                        <th className="px-4 py-2">Location</th>
                        <th className="px-4 py-2">Food Category</th>
                        <th className="px-4 py-2">Qty</th>
                        <th className="px-4 py-2">Order Status</th>
                        {userRole !== 'Customer' && <th className="px-4 py-2">Update Status</th>}
                    </tr>
                </thead>
                <tbody>
                    {filteredOrders.map((order) => (
                        <tr className="text-gray-700 hover:bg-gray-100">
                            <td className="px-4 py-2">{order.id}</td>
                            <td className="px-4 py-2">{order.pickupTime}</td>
                            <td className="px-4 py-2">{order.location}</td>
                            <td className="px-4 py-2">{order.foodCategory}</td>
                            <td className="px-4 py-2">{order.qty}</td>
                            <td className="px-4 py-2">{order.orderStatusId}</td>
                            <td className="px-4 py-2">

                                {/* {orderStatus === "" ? <div>
                                <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 2)}
                                        >
                                            Accept
                                        </button>

                                        <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 3)}
                                        >
                                            Reject
                                        </button>
                                </div> : <></> } */}

                                {orderStatus === "Pending" ?

                                    <div>
                                        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 2)}
                                        >
                                            Accept
                                        </button>

                                        <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 3)}
                                        >
                                            Reject
                                        </button>
                                    </div> : <></>
                                }

                                {orderStatus === "Paid" ?
                                    <div>
                                        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 2)}
                                        >
                                            Accept
                                        </button>

                                        <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 3)}
                                        >
                                            Reject
                                        </button>
                                    </div>
                                    : <></>
                                }

                                {orderStatus === "Accepted" ?
                                    <div>
                                        <button className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                            onClick={() => updateOrderStatus(order.id, 4)}
                                        >
                                            Complete
                                        </button>

                                    </div> : <></>
                                }

                                {orderStatus === "Completed" && order.serviceTypeId !== "Self Pickup" ?

                                    <button className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 11)}
                                    >
                                        Delivery Acceptance
                                    </button> : <></>

                                }

                                {orderStatus === "Completed" && order.serviceTypeId === "Self Pickup" ?
                                    <button class="bg-amber-400 hover:bg-amber-600 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 5)}
                                    >
                                        Done
                                    </button> : <></>
                                }

                                {orderStatus === "Incompleted" ?
                                    <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 4)}
                                    >
                                        Complete
                                    </button>
                                    : <></>
                                }

                                {orderStatus === "Delivery Accepted" && order.serviceTypeId === "Delivery" ?
                                    <button className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 7)}
                                    >
                                        On the way
                                    </button> : <></>
                                }

                                {orderStatus === "Delivery Accepted" && order.setOrderStatus === "Delivery Free" ?
                                    <button className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 7)}
                                    >
                                        On the way
                                    </button> : <></>
                                }
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}