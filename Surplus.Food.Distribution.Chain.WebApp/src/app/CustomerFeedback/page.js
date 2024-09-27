"use client";
import React from "react";
import axios from "axios"; // Import axios for making HTTP requests
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";
import { useState, useEffect } from "react";
import "react-toastify/dist/ReactToastify.css";
import * as Yup from 'yup';

export default function CustomerFeedback() {

    const [reviews, setReview] = useState([]);

    var supId = ""

    if (typeof window !== "undefined") {
        supId = localStorage.getItem('supplierId');
    } else {
        console.log("localStorage is not available in this context.");
    }

    function fetchData() {
        axios.get(`${apiUrl}customer/reviews/${supId}`)
                .then(response => {
                    setReview(response.data);
                    console.log(response.data);
                })
                .catch(error => {
                    console.error(error);
                });
    }

    useEffect(() => {
        fetchData();
    }, []);

    return(
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h1 class="text-yellow-950 mb-5"> Customer Feedbacks</h1>
                <hr></hr>
            </div>


            <table className="table-auto w-full mt-2 border border-gray-300">
                <thead>
                    <tr className="bg-gray-200 text-left">
                        <th className="px-4 py-2">Customer Name</th>
                        <th className="px-4 py-2">Rating</th>
                        <th className="px-4 py-2">Review</th>
                    </tr>
                </thead>
                <tbody>
                    {reviews.map((review) => (
                        <tr key={review.id} className="text-gray-700 hover:bg-gray-100">
                            <td className="px-4 py-2">{review.customerName}</td>
                            <td className="px-4 py-2">{review.rating}</td>
                            <td className="px-4 py-2">{review.review}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <ToastContainer />
        </div>
    );
}