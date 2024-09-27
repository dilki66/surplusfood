'use client';
import React from "react";
import axios from "axios"; // Import axios for making HTTP requests
import * as Yup from 'yup';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { toast, ToastContainer } from 'react-toastify'; // Import react-toastify
import apiUrl from "@/app/config/apiurl";

function CustomerForm(role) {
    const validationSchema = Yup.object().shape({
        firstName: Yup.string().required('First Name is required'),
        lastName: Yup.string().required('Last Name is required'),
        address: Yup.string().required('Address is required'),
        contactNo: Yup.string().required('Contact No is required'),
        email: Yup.string().email('Invalid email').required('Email is required'),
        password: Yup.string()
          .required('Password is required')
          .matches(
            /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
            'Password must be at least 8 characters and contain at least one letter, one number, and one special character'
          ),
        confPassword: Yup.string()
          .oneOf([Yup.ref('password'), null], 'Passwords must match')
          .required('Confirm Password is required')
          .matches(
            /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
            'Password must be at least 8 characters and contain at least one letter, one number, and one special character'
        ),
      });
    return (
        <div>
            <Formik
                initialValues={{ role: "Customer", firstName: "", lastName: "", address: "", contactNo: "", nic: "", email: "", password: "", confPassword: "" }}
                validationSchema={validationSchema}
                onSubmit={(values, { setSubmitting, resetForm }) => {
                    // Make a POST request to your backend API
                    console.log(values);
                    axios
                        .post(`${apiUrl}auth/register`, values)
                        .then((response) => {
                            // Handle successful login
                            toast.success("Registration successful!");
                            console.log(response.data); // You can access response data here

                            resetForm(); // Reset form fields after successful login
                            window.location.href = "/login";

                        })
                        .catch((error) => {
                            // Handle errors
                            toast.error("Registration failed. Please try again."); // Display error message using toastify
                            console.error("Registration error:", error);
                        })
                        .finally(() => {
                            setSubmitting(false); // Reset submitting state
                        });
                }}
            >
                {({ isSubmitting }) => (
                    <Form>
                        <div className="grid grid-cols-4 gap-2">
                            <div className="mb-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">First Name <span className="text-red-700">*</span></label>
                                <Field type="text" name="firstName" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="firstName" component="div" className="text-red-500 text-xs italic" />
                            </div>
                            <div className="mb-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Last Name <span className="text-red-700">*</span></label>
                                <Field type="text" name="lastName" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="lastName" component="div" className="text-red-500 text-xs italic" />
                            </div>
                            <div className="mb-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Address <span className="text-red-700">*</span></label>
                                <Field type="text" name="address" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="address" component="div" className="text-red-500 text-xs italic" />
                            </div>
                            <div className="mb-4">
                                <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Contact No <span className="text-red-700">*</span></label>
                                <Field type="text" name="contactNo" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="contactNo" component="div" className="text-red-500 text-xs italic" />
                            </div>
                        </div>

                        <div className="grid grid-cols-4 gap-2">
                            <div className="mb-4">
                                <label htmlFor="nic" className="block text-gray-700 text-sm font-bold mb-2">NIC</label>
                                <Field type="text" name="nic" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="nic" component="div" className="text-red-500 text-xs italic" />
                            </div>
                            <div className="mb-4">
                                <label htmlFor="email" className="block text-gray-700 text-sm font-bold mb-2">Email <span className="text-red-700">*</span></label>
                                <Field type="email" name="email" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-xs italic" />
                            </div>
                            <div className="mb-6-">
                                <label htmlFor="password" className="block text-gray-700 text-sm font-bold mb-2">Password <span className="text-red-700">*</span></label>
                                <Field type="password" name="password" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="password" component="div" className="text-red-500 text-xs italic max-w-xs" />
                            </div>
                            <div className="mb-6-">
                                <label htmlFor="conpassword" className="block text-gray-700 text-sm font-bold mb-2">Confirm Password <span className="text-red-700">*</span></label>
                                <Field type="password" name="confPassword" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                                <ErrorMessage name="confPassword" component="div" className="text-red-500 text-xs italic max-w-xs" />
                            </div>
                        </div>

                        <div className="flex items-center justify-between">
                            <button type="submit" disabled={isSubmitting} className="bg-yellow-900 hover:bg-yellow-950 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-5">
                                {isSubmitting ? 'Registering...' : 'Register'}
                            </button>
                        </div>
                    </Form>
                )}
            </Formik>
            <ToastContainer /> {/* Toast container for displaying toast messages */}
        </div>
    )
}

export default CustomerForm;