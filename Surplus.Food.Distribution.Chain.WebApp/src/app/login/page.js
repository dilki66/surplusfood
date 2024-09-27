"use client";
import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios"; // Import axios for making HTTP requests
import { toast, ToastContainer } from "react-toastify"; // Import react-toastify
import "react-toastify/dist/ReactToastify.css"; // Import react-toastify styles
import backgroundImage from "../../../public/images/authBack.png";
import styles from "./login.module.css";
import apiUrl from "../config/apiurl";

function Login() {

  function loginAsGuest() {
    axios
      .post(`${apiUrl}auth/login`, {
        email: "guest@surplus.com",
        password: "Super@123"
      })
      .then((response) => {

        if (response.status === 200) {
          // Handle successful login
          toast.success("Login successful!");
          console.log(response.data); // You can access response data here

          localStorage.setItem('token', response.data.token)
          localStorage.setItem('email', response.data.email)
          localStorage.setItem('role', response.data.role)
          localStorage.setItem('userId', response.data.userId)
          localStorage.setItem('supplierId', response.data.supplierId)
          localStorage.setItem('address', response.data.customerAddress)

          window.location.href = "/";

        } else {
          toast.error("Login failed. Please try again."); // Display error message using toastify
        }

      })
  }

  return (
    <div className={styles.loginBackground}>
      <div className="flex justify-center items-center h-screen">
        <div className={styles.card}>
          <Formik
            initialValues={{ email: "", password: "" }}
            validationSchema={Yup.object({
              email: Yup.string()
                .email("Invalid email address")
                .required("Required"),
              password: Yup.string().required("Required"),
            })}
            onSubmit={(values, { setSubmitting, resetForm }) => {
              // Make a POST request to your backend API
              axios
                .post(`${apiUrl}auth/login`, values)
                .then((response) => {

                  if (response.status === 200) {
                    // Handle successful login
                    toast.success("Login successful!");
                    console.log(response.data); // You can access response data here

                    localStorage.setItem('token', response.data.token)
                    localStorage.setItem('email', response.data.email)
                    localStorage.setItem('role', response.data.role)
                    localStorage.setItem('userId', response.data.userId)
                    localStorage.setItem('supplierId', response.data.supplierId)
                    localStorage.setItem('supplierName', response.data.supplierName)
                    localStorage.setItem('address', response.data.customerAddress)

                    resetForm(); // Reset form fields after successful login

                    window.location.href = "/";

                  } else {
                    toast.error("Login failed. Please try again."); // Display error message using toastify
                  }

                })
                .catch((error) => {
                  // Handle errors
                  toast.error("Login failed. Please try again."); // Display error message using toastify
                  console.error("Login error:", error);
                })
                .finally(() => {
                  setSubmitting(false); // Reset submitting state
                });
            }}
          >
            {({ isSubmitting }) => (
              <Form className="mb-3">
                <div className="mb-4 text-center">
                  <h3 className="block text-gray-700 text-sm font-bold text-lg mb-2">
                    Log In
                  </h3>
                </div>
                <div className="mb-4">
                  <label
                    htmlFor="email"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Email
                  </label>
                  <Field
                    type="email"
                    name="email"
                    className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                  />
                  <ErrorMessage
                    name="email"
                    component="div"
                    className="text-red-500 text-xs italic"
                  />
                </div>
                <div className="mb-6-">
                  <label
                    htmlFor="password"
                    className="block text-gray-700 text-sm font-bold mb-2"
                  >
                    Password
                  </label>
                  <Field
                    type="password"
                    name="password"
                    className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                  />
                  <ErrorMessage
                    name="password"
                    component="div"
                    className="text-red-500 text-xs italic"
                  />
                </div>
                <div className="flex items-center justify-between mt-5">
                  <button
                    type="submit"
                    disabled={isSubmitting}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                  >
                    {isSubmitting ? "Logging in..." : "Log In"}
                  </button>
                </div>

                <a className="mt-10 text-blue-500 underline-offset-1" href="/register">Register</a>
              </Form>
            )}
          </Formik>
          <ToastContainer /> {/* Toast container for displaying toast messages */}

          {/* <div>
            <button className="bg-amber-500 hover:bg-amber-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
              onClick={() => loginAsGuest()}
            >
              Login as Guest
            </button>
          </div> */}
        </div>
      </div>
    </div>
  );
}

export default Login;
