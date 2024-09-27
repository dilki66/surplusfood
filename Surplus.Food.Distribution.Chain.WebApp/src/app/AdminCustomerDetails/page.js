"use client";
import React, { useState, useEffect } from "react";
import axios from "axios";
import { toast, ToastContainer } from "react-toastify"; // Import react-toastify
import "react-toastify/dist/ReactToastify.css";

function AdminCustomerDetails() {

    const [datacus, setDataCus] = useState([]);

    useEffect(() => {
        fetchDataCustomer();
      }, []);

    const fetchDataCustomer = async () => {
        try {
          const response = await axios.get(
            "https://localhost:7044/api/customer/details"
          );
          setDataCus(response.data);
        } catch (error) {
          console.error("Error fetching data:", error);
        }
      };

    const handleToggleStatusCustomer = async(cus) => {
        // Your logic for toggling status
        if(cus.status === false){
          await axios.put(`https://localhost:7044/api/Admin/customer/disable/${cus.id}`).then(response => {
            toast.success("Status Update Successfully");
            window.location.reload();
          });
         }    
        
      };

    return (
        <div className="ms-80 mt-20 mr-20">
            <div className="grid-col text-3xl font-bold">
                <h1 className="text-yellow-950 mb-5">Admin</h1>
                <hr></hr>
            </div>


            <div className="grid-col  font-bold">
        <h3 className="text-yellow-950 mt-10 mb-2">Customer Details Table</h3>       
        
      </div>

      <table className="table-auto w-full border border-gray-300 mt-5">
        <thead>
          <tr className="bg-gray-200 text-left">
            <th className="px-4 py-2">Customer Name</th>
            <th className="px-4 py-2">Contact Number</th>           
            <th className="px-4 py-2">Status</th>
            {/* <th className="px-4 py-2">Actions</th> */}
          </tr>
        </thead>
        <tbody>
          {datacus?.map((cus) => (
            <tr key={cus.id} className="text-gray-700 hover:bg-gray-100">
              <td className="px-4 py-2">{cus?.name}</td>
              <td className="px-4 py-2">{cus?.contactNo}</td>              
              <td className="px-4 py-2">
              <button
                  className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                  onClick={() => handleToggleStatusSupplier(order)}
                >
                   Enable
                </button>

                <button
                  className="bg-red-500 hover:bg-red-700 text-white font-bold ml-3 py-2 px-4 rounded"
                  onClick={() => handleToggleStatusSupplier(order)}
                >
                   Disable
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
        </div>
    );
}

export default AdminCustomerDetails;