'use client';
import React from "react";
import styles from './register.module.css';
import { ToastContainer } from 'react-toastify'; // Import react-toastify
import 'react-toastify/dist/ReactToastify.css'; // Import react-toastify styles
import { useState, useEffect } from "react";
import FoodSupplierForms from "../components/FoodSupplierForms/page";
import CustomerForm from "../components/CustomerForm/page";
import DeliveryPersonForm from "../components/DeliveryPersonForm/page";
import DonorForm from "../components/DonorForm/page";
import { Formik, Form, Field, ErrorMessage } from 'formik';

function Register({ initialRole = "FoodSupplier" }) {

    const [role, setRole] = useState(initialRole || "FoodSupplier");

    const handleRoleChange = (event) => {
        setRole(event.target.value); // Update the role state with the selected value
    };

    useEffect(() => {
        setRole(window.role || "FoodSupplier");
    }, []);

    return (
        <Formik>
            <div className={styles.loginBackground}>
                <div className="flex justify-center items-center h-screen">
                    <div className={styles.card}>
                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Select a Role</label>
                            <select
                                id="roles"
                                className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                onChange={handleRoleChange}
                                value={role}
                            >
                                <option value="FoodSupplier">Food Supplier</option>
                                <option value="Customer">Customer</option>
                                <option value="DeliveryPerson">Delivery Person</option>
                                <option value="Guest">Donor</option>
                             
                            </select>
                        </div>

                        {role === "FoodSupplier" && <FoodSupplierForms />}
                        {role === "Customer" && <CustomerForm />}
                        {role === "DeliveryPerson" && <DeliveryPersonForm />}
                        {role === "Guest" && <DonorForm />}
                        <a className="mt-10 text-blue-500 underline-offset-1" href="/login">Login</a>
                    </div>
                </div>
           

            </div>

        </Formik>
    );
}

export default Register;