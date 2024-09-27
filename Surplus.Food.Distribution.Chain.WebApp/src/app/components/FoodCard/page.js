import React, { useState } from "react";
import Image from "next/image";
import styles from "./foodCard.module.css";
import axios from "axios";
import { toast, ToastContainer } from "react-toastify"; // Import react-toastify
import "react-toastify/dist/ReactToastify.css"; 

export default function FoodCard(food) {
  const [qty, setQty] = useState(1);
  const [showbtn, setShowbtn] = useState(true);

  var serviceTypeId = 0;

  if (food.serviceTypeId === "Delivery") {
    serviceTypeId = 1;
  }
  else if (food.serviceTypeId === "Self Pickup") {
    serviceTypeId = 2;
  }
  else{
    serviceTypeId = 3;
  }

  const userid = localStorage.getItem("userId");

  const handleIncrement = () => {
    if( qty < food?.quantity ){
      setQty(qty + 1);
     
    }
    else
    {
      setShowbtn(false);
    }
    
  };

  const handleDecrement = () => {
    setQty(Math.max(0, qty - 1));
  };

  const getImageSource = (imageData) => {
    console.log({ imageData });
    if (imageData && Array.isArray(imageData)) {
      const base64String = btoa(String.fromCharCode(...imageData));
      return `data:image/png;base64,${base64String}`;
    }
    return `data:image/png;base64,${imageData}`;
  };

  const addToCart = () => {
    const requestBody = {
      foodItems: [
        {
          id: food.id, // Assuming food object has an 'id' property
          quantity: qty,
          amount: food.price, // Assuming 'price' is a property of the food object
        },
      ],
      userId: userid,
      serviceTypeId: serviceTypeId,
    };

    // Make the API call using Axios
    axios.post("https://localhost:7044/api/order/cart", requestBody)
      .then((response) => {
        // Handle success
        
        toast.success("Item added to cart successfully!");
      })
      .catch((error) => {
        // Handle error
        console.error("Error adding item to cart:", error);
      });
  };

  return (
    <div className="w-full max-w-xl bg-white border border-gray-200 rounded-lg shadow flex mb-8">
      {/* Column 1: Image */}
      <ToastContainer />
      <div>
        <img
          src={getImageSource(food?.image)}
          alt="gg"
          style={{
            width: "200px",
            height: "205px",
            backgroundSize: "cover",

            backgroundRepeat: "no-repeat",
          }}
        />
      </div>

      {/* Column 2: Food Details */}
      <div className="w-1/3 p-4">
        <h3 className="text-lg font-medium">{food?.category}</h3>
        <span className="text-gray-500">{food?.price}</span>
        {/* <div className="text-gray-500 text-sm mb-2">
          Pizza Hut · Colombo 03
        </div> */}
      </div>

      {/* Column 3: Quantity & Add to Cart */}
      <div className="w-1/3 p-4 flex flex-col ">
        <div className="mb-2">
          <label htmlFor="qty" className="sr-only">
            {food?.quantity}
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
        <button onClick={addToCart}  className="bg-yellow-900 text-white font-medium py-2 px-4 rounded hover:bg-yellow-950">
          Add to Cart
        </button> 
      </div>
    </div>
  );
}
