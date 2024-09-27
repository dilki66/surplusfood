"use client";

import React from 'react';
import FoodCard from '../components/FoodCard/page';

export default function FoodResults() {
  // Sample food data (Replace with your actual data source)
  const foodData = [
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    { name: 'Chicken Pizza', image: '/path/to/image1.jpg', price: '2200', supplier: 'Pizza Hut', location: 'Colombo 03' },
    { name: 'Nasi Goreng', image: '/path/to/image2.jpg', price: '1200', supplier: 'The Lankan Cafe', location: 'Kandy' },
    // ... more food items
  ];

  return (
    <div className="ms-80 mt-20 mr-20">
      <div className="grid-col text-3xl font-bold">
        <h1 className="text-yellow-950 mb-5">Search Results</h1>
        <hr />
      </div>

      <div className="mt-10 grid grid-cols-2 gap-4"> {/* Using grid for columns */}
        {foodData.map((food) => (
          <FoodCard key={food.name} {...food} />
        ))}
      </div>
    </div>
  );
}
