"use client";
import apiUrl from "@/app/config/apiurl";
import axios from "axios";
import React, { useState, useEffect } from 'react';
import "react-toastify/dist/ReactToastify.css";
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import jsPDF from 'jspdf';
import 'jspdf-autotable';



function OrderList() {

    const [orders, setOrders] = useState([]);
    const [role, setRole] = useState();

    const logedUserRole = localStorage.getItem('role');

    function fetchData() {
        const page = 1;

        if (logedUserRole === 'DeliveryPerson') {
            axios.get(`${apiUrl}order/delivery/${page}`)
                .then(response => {
                    setOrders(response.data.orders);
                    console.log(response.data.orders);
                })
                .catch(error => {
                    console.error(error);
                });
        }
        else if (logedUserRole === 'Customer') {
            const userId = localStorage.getItem('userId');
            axios.get(`${apiUrl}order/customer/${userId}/${page}`)
                .then(response => {
                    setOrders(response.data.orders);
                    console.log(response.data.orders);
                })
                .catch(error => {
                    console.error(error);
                });
        }
        else {
            axios.get(`${apiUrl}order/${page}`)
                .then(response => {
                    setOrders(response.data.orders);
                    console.log(response.data.orders);
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }

    const [selectedLocation, setSelectedLocation] = useState('');

    const handleLocationChange = (event) => {
        setSelectedLocation(event.target.value);
    };

    const filteredOrders = orders.filter((order) => {
        return selectedLocation === '' || order.location === selectedLocation;
    });

    useEffect(() => {
        const userRole = localStorage.getItem('role');
        setRole(userRole);

        fetchData();
    }, []);

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
    function generatePDF() {
        const doc = new jsPDF();
    
        // Define column headers
        const headers = [['Pickup Time','Location','Reciever Name']];
    
        // Define rows from orders data
        const rows = orders.map(order => [
            order.pickupTime,
            order.location,
            
            order.recieverName                             
        ]);
    
        // Add table to PDF
        doc.autoTable({
            head: headers,
            body: rows
        });
    
        // Save or download the PDF
        doc.save('order_list.pdf');
    }
    
    return (
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h1 class="text-yellow-950 mb-5"> Order List</h1>
                <button onClick={generatePDF}>Save as PDF </button>
                <hr></hr>
            </div>

            {
                role === "DeliveryPerson" ?
                    <select
                        class="shadow appearance-none border rounded py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mt-10 p-2"
                        value={selectedLocation}
                        onChange={handleLocationChange}
                    >
                        <option value="">All Locations</option>
                        {Array.from(new Set(orders.map((order) => order.location))).map((location) => (
                            <option key={location} value={location}>
                                {location}
                            </option>
                        ))}
                    </select>
                    : <></>
            }


            <table className="table-auto w-full mt-2 border border-gray-300">
                <thead>
                    <tr className="bg-gray-200 text-left">
                        {role === 'DeliveryPerson' ? <th className="px-4 py-2">Order Id</th> : <></>}
                        {role === 'DeliveryPerson' ? <th className="px-4 py-2">Food Supplier</th> : <></>}
                        {role === 'DeliveryPerson' ? <th className="px-4 py-2">Food Supplier Address</th> : <></>}
                        {role === 'DeliveryPerson' ? <th className="px-4 py-2">Customer Address</th> : <></>}
                        {role === 'DeliveryPerson' ? <th className="px-4 py-2">Customer Contact No</th> : <></>}
                        <th className="px-4 py-2">Pickup Time</th>
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">Location</th> : <></>}
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">Sender Name</th> : <></>}
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">Receiver Name</th> : <></>}
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">Service Type</th> : <></>}
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">Payment Method</th> : <></>}
                        {role !== 'DeliveryPerson' ? <th className="px-4 py-2">CreatedAt</th> : <></>}
                        <th className="px-4 py-2">Order Status</th>
                        {role !== 'Admin' ? <th className="px-4 py-2">Update Status</th> : <></>}
                    </tr>
                </thead>
                <tbody>
                    {filteredOrders.map((order) => (
                        <tr key={order.id} className="text-gray-700 hover:bg-gray-100">
                            {role === 'DeliveryPerson' ? <td className="px-4 py-2">{order.id}</td> : <></>}
                            {role === 'DeliveryPerson' ? <td className="px-4 py-2">{order.foodSupplierName}</td> : <></>}
                            {role === 'DeliveryPerson' ? <td className="px-4 py-2">{order.foodSupplierAddress}</td> : <></>}
                            {role === 'DeliveryPerson' ? <td className="px-4 py-2">{order.location}</td> : <></>}
                            {role === 'DeliveryPerson' ? <td className="px-4 py-2">{order.customerContactNo}</td> : <></>}
                            <td className="px-4 py-2">{order.pickupTime}</td>
                            {role !== 'DeliveryPerson' ? <td className="px-4 py-2">{order.location}</td> : <></>}
                            {role !== 'DeliveryPerson' ? <td className="px-4 py-2">{order.senderName}</td> : <></>}
                            {role !== 'DeliveryPerson' ? <td className="px-4 py-2">{order.recieverName}</td> : <></>}
                            {role !== 'DeliveryPerson' ? <td className="px-4 py-2">{order.serviceTypeId}</td> : <></>}
                            {role !== 'DeliveryPerson' ? <td className="px-4 py-2">{order.paymentMethod}</td> : <></>}
                           
                            <td className="px-4 py-2">{order.orderStatusId}</td>
                            {role !== 'Admin' ? <td className="px-4 py-2">
                                {
                                    role !== 'Customer' && role !== 'DeliveryPerson' && <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 1)}
                                    >
                                        Pending
                                    </button>
                                }

                                {
                                    role !== 'Customer' && role !== 'DeliveryPerson' && <button class="bg-amber-400 hover:bg-amber-600 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 5)}
                                    >
                                        Done
                                    </button>
                                }

                                {
                                    role !== 'Customer' && <button class="bg-blue-400 hover:bg-blue-600 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 8)}
                                    >
                                        Accept
                                    </button>
                                }

                                {
                                    role !== 'Customer' && role !== 'DeliveryPerson' && <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 4)}
                                    >
                                        Completed
                                    </button>
                                }

                                {
                                    role !== 'Customer' && <button class="bg-red-600 hover:bg-red-800 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2"
                                        onClick={() => updateOrderStatus(order.id, 10)}
                                    >
                                        Rejected
                                    </button>
                                }

                                {
                                    role === 'Customer' && order.orderStatusId == "On the way" && <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 9)}
                                    >
                                        Recieved
                                    </button>
                                }

                                {/* {
                                    role === 'Customer' && order.orderStatusId == "Done" && <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline mt-2 mr-2"
                                        onClick={() => updateOrderStatus(order.id, 9)}
                                    >
                                        Recieved
                                    </button>
                                } */}
                            </td> : <></>}
                        </tr>
                    ))}
                </tbody>
            </table>
            <ToastContainer />
        </div>
    );
}

export default OrderList;