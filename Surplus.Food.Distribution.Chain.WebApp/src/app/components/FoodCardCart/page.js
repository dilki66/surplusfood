'use client';
import React, { useEffect, useState } from 'react';
import Image from 'next/image';
import styles from './foodCardCart.module.css';
import { toast, ToastContainer } from "react-toastify"; // Import react-toastify
import axios from 'axios';
import apiUrl from '@/app/config/apiurl';


export default function FoodCardCart(props) {
  const [qty, setQty] = useState(0);

 


  const handleIncrement = () => {
    setQty(qty + 1);
  };

  const handleDecrement = () => {
    setQty(Math.max(0, qty - 1));
  };

  useEffect(() => {

    setQty(props.foods.quantity);

  }, []);



  function deleteFoodItem(itemId) {

    const userId = localStorage.getItem('userId');


    axios.delete(`${apiUrl}order/cart/${userId}/${itemId}`)
      .then(response => {
        console.log('Food item deleted:', response.data);
        toast.success("Food item removed from cart!");
        window.location.reload();
      })
      .catch(error => {
        console.error('Error deleting food item:', error);
        toast.error("Error removing food item from cart!");
      });
  }

  return (
    <div className="w-full max-w-xl bg-white border border-gray-200 rounded-lg shadow flex mb-8">
      {/* Column 1: Image */}
      <div className={styles.cardImage} >

      </div>

      {/* Column 2: Food Details */}
      <div className="w-1/3 p-4">
        <h3 className="text-lg font-medium">{props.foods.foodItemName}</h3>
        <h4 className="mt-2">Total: {props.foods.amount * qty} LKR</h4>
        {props.foods.serviceTypeId === 1 ? <span className="text-gray-500">Delivery: 200 LKR</span> : null}
      </div>

      {/* Column 3: Quantity & Add to Cart */}
      <div className="w-1/3 p-4 flex flex-col ">
        <div className="mb-2">
          <label htmlFor="qty" className="sr-only">
            Quantity
          </label>
          <div className="flex items-center mb-4">
            <button
              className=" mr-1 px-2 w-8 py-1 border border-gray-300 bg-gray-100 hover:bg-gray-200"
              onClick={handleDecrement}
            >
              -
            </button>
            <input
              id="qty"
              type="number"
              value={qty}
              readOnly
              className="w-10 h-8 text-center border border-gray-300 focus:outline-none focus:ring"
            />
            <button
              className=" ms-1 px-2 py-1 w-8 border border-gray-300 bg-gray-100 hover:bg-gray-200"
              onClick={handleIncrement}
            >
              +
            </button>
          </div>
        </div>
        <button onClick={() => { deleteFoodItem(props.foods.foodItemId) }} className="bg-yellow-900 text-white font-medium py-2 px-4 rounded hover:bg-yellow-950">
          Remove
        </button>
      </div>
      
    </div>
  );
}
