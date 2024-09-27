"use client";
import React, { useState, useEffect } from "react";
import axios from "axios";
import { toast, ToastContainer } from "react-toastify"; // Import react-toastify
import "react-toastify/dist/ReactToastify.css";

function Admin() {

  const [datasup, setDataSup] = useState([]);


  useEffect(() => {
    fetchDataSupplier();
  }, []);

  const fetchDataSupplier = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7044/api/supplier/supplier"
      );
      setDataSup(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }; 

  const handleToggleStatusSupplier = async(order) => {

    await axios.put(`https://localhost:7044/api/Admin/food-supplier/disable/${order.id}`).then(response => {
      toast.success("Status Update Successfully");
      window.location.reload();
    });   
  };

  return (
    <div className="ms-80 mt-20 mr-20">
      <ToastContainer />
      <div className="grid-col text-3xl font-bold">
        <h1 className="text-yellow-950 mb-5"> Admin Tables</h1>
        <hr />
      </div>

      <div className="grid-col  font-bold">
        <h3 className="text-yellow-950 mt-10 mb-2">Food Supplier Table</h3>       
        
      </div>

      <table className="table-auto w-full border border-gray-300 mt-5">
        <thead>
          <tr className="bg-gray-200 text-left">
            <th className="px-4 py-2">Food Supplier Name</th>
            <th className="px-4 py-2">Owner Name</th>
            <th className="px-4 py-2">Contact No</th>
            <th className="px-4 py-2">Status</th>
            {/* <th className="px-4 py-2">Actions</th> */}
          </tr>
        </thead>
        <tbody>
          {datasup?.map((order) => (
            <tr key={order.id} className="text-gray-700 hover:bg-gray-100">
              <td className="px-4 py-2">{order?.suplierName}</td>
              <td className="px-4 py-2">{order?.ownerName}</td>
              <td className="px-4 py-2">{order?.contactNo}</td>
              
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

export default Admin;
