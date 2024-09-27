"use client";
import React from "react";
import axios from "axios"; // Import axios for making HTTP requests
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";
import { useState, useEffect } from "react";
import "react-toastify/dist/ReactToastify.css";
import * as Yup from 'yup';

export default function CustomerReview() {

    const [suupliers, setSuppliers] = useState([]);
    const [review, setReview] = useState("");
    const [rating, setRating] = useState(0);

    useEffect(() => {
        axios
            .get("https://localhost:7044/api/supplier/supplier")
            .then((response) => {
                console.log(response.data);
                setSuppliers(response.data);
            });
    }, []);

    var userId = ""

    if (typeof window !== "undefined") {
        userId = localStorage.getItem('userId');
    } else {
        console.log("localStorage is not available in this context.");
    }

    return (
        <div className=" ms-80 mt-20 mr-20">
            <div class="grid-col text-3xl font-bold">
                <h2 class="text-yellow-950 mb-5">Add Supplier Review</h2>
                <hr></hr>
            </div>

            <Formik
                initialValues={{
                    supplierId: suupliers.length > 0 ? suupliers[0].id : "",
                    rating: 0,
                    review: review
                }}
                validationSchema={Yup.object({})}
                onSubmit={(values) => {
                    console.log({ values })
                    axios
                        .post(`${apiUrl}customer/review?userId=${userId}`, values)
                        .then((response) => {
                            console.log(response.data);
                            toast.success("Review added successfully");
                        })
                        .catch((error) => {
                            console.log(error);
                            toast.error("Review adding failed. Please try again.");
                        });

                }}
                enableReinitialize={true}
            >
                {({ values, handleChange, isSubmitting }) => (
                    <Form>
                        <div class="grid grid-cols-3 mt-5"></div>
                        <div class="grid grid-cols-3">
                            <div class="col-span-1">
                                <label class="text-yellow-950 font-bold">Supplier Name</label>
                            </div>
                            <div class="col-span-2 mb-5">
                                <select
                                    id="supplierId"
                                    className="shadow appearance-none border rounded border-2 border-yellow-950 rounded-md w-96 h-10 py-2 px-4 leading-tight focus:outline-none focus:shadow-outline"
                                    onChange={handleChange}
                                    value={values.supplierId}
                                >
                                    {suupliers?.map((supplier) => (
                                        <option value={supplier?.id} key={supplier.id}>
                                            {supplier?.suplierName}
                                        </option>
                                    ))}
                                </select>
                            </div>

                            <div class="col-span-1">
                                <label class="text-yellow-950 font-bold">Rating</label>
                            </div>
                            <div class="col-span-2 mb-5">
                                <div >
                                    <input
                                        type="text"
                                        name="rating"
                                        className="shadow appearance-none border rounded border-2 border-yellow-950 rounded-md w-96 h-10 py-2 px-4 leading-tight focus:outline-none focus:shadow-outline"
                                        value={values.rating}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="rating" component="div" className="text-red-500 text-xs italic" />
                                </div>
                            </div>

                            <div class="col-span-1">
                                <label class="text-yellow-950 font-bold">Review</label>
                            </div>

                            <Field
                                as="textarea"
                                name="review"
                                id="message"
                                rows="4"
                                className="shadow appearance-none border rounded border-2 border-yellow-950 rounded-md w-96 block p-2.5 w-full rounded-lg border-2 border-yellow-950 focus:ring-amber-700 focus:border-amber-700"
                                placeholder="Write your thoughts here..."
                            />
                        </div>


                        <div className="mb-4 mt-10 w-80">
                            <button type="submit" disabled={isSubmitting} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2">
                                {isSubmitting ? 'Adding...' : 'Add a Review'}
                            </button>
                        </div>
                    </Form>
                )}
            </Formik>
            <ToastContainer />

        </div>
    );
}