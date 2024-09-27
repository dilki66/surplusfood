'use client';
import React, { useEffect, useState } from "react";
import Image from 'next/image'
import styles from './navbar.module.css';
import { FiUsers } from "react-icons/fi";
import { LuSearchCode } from "react-icons/lu";
import { TiThListOutline } from "react-icons/ti";
import { MdOutlineShoppingCart } from "react-icons/md";
import { MdOutlineLogin } from "react-icons/md";
import { MdLogout } from "react-icons/md";
import { LuApple } from "react-icons/lu";
import { LuLayoutDashboard } from "react-icons/lu";
import { BiDonateHeart } from "react-icons/bi";
import { MdOutlinePersonRemove } from "react-icons/md";
import { LiaShuttleVanSolid } from "react-icons/lia";
import { LiaDonateSolid } from "react-icons/lia";
import { FaRegEdit } from "react-icons/fa";
import { VscFeedback } from "react-icons/vsc";



function Navbar() {


  const [role, setRole] = useState();

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("email");
    localStorage.removeItem("role");
    localStorage.removeItem("userId");
    localStorage.removeItem("supplierId");
    window.location.href = "/login";
  }



  useEffect(() => {

    const token = localStorage.getItem("token");

    if (!token) {
      console.log("token", token);
      window.location.href = "/login";
    }


    const userRole = localStorage.getItem("role");

    setRole(userRole);

  }, []);



  return (
    <div >
      <button
        data-drawer-target="default-sidebar"
        data-drawer-toggle="default-sidebar"
        aria-controls="default-sidebar"
        type="button"
        class="inline-flex items-center p-2 mt-2 ms-3 text-sm text-gray-500 rounded-lg sm:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
      >
        <span class="sr-only">Open sidebar</span>
        <svg
          class="w-6 h-6"
          aria-hidden="true"
          fill="currentColor"
          viewBox="0 0 20 20"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            clip-rule="evenodd"
            fill-rule="evenodd"
            d="M2 4.75A.75.75 0 012.75 4h14.5a.75.75 0 010 1.5H2.75A.75.75 0 012 4.75zm0 10.5a.75.75 0 01.75-.75h7.5a.75.75 0 010 1.5h-7.5a.75.75 0 01-.75-.75zM2 10a.75.75 0 01.75-.75h14.5a.75.75 0 010 1.5H2.75A.75.75 0 012 10z"
          ></path>
        </svg>
      </button>

      <aside
        id="default-sidebar"
        class="fixed top-0 left-0 z-40 w-64 h-screen transition-transform -translate-x-full sm:translate-x-0 border-r border-yellow-950"
        aria-label="Sidebar"
      >
        <div class="h-full px-3 py-4 overflow-y-auto border-orange-950 ">


          <div class="content-center ml-10">
            <div className={styles.logoImage} ></div>
          </div>
          <ul class="space-y-2 font-medium mt-10">


            {role === "Admin" ?

              <div>

                <li>
                  <a
                    href="admin"
                    class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                  >
                    <LiaShuttleVanSolid class="text-yellow-950" size={22} />
                    <span class="ms-3 text-amber-800">Food Supplier Details</span>
                  </a>
                </li>

                <li>
                  <a
                    href="AdminCustomerDetails"
                    class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                  >
                    <MdOutlinePersonRemove class="text-yellow-950" size={22} />
                    <span class="ms-3 text-amber-800">Customer Details</span>
                  </a>
                </li>

                {/* <li>
                  <a
                    href="AdminDonorDetails"
                    class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                  >
                    <LiaDonateSolid class="text-yellow-950" size={22} />
                    <span class="ms-3 text-amber-800">Donor Details</span>
                  </a>
                </li>  */}
              </div> : <></>

            }

            {role === "Customer" ? <div>

              <li>
                <a
                  href="SearchFood"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <LuSearchCode class="text-yellow-950" size={22} />
                  <span class="ms-3 text-amber-800">Search Foods</span>
                </a>
              </li>

              <li>
                <a
                  href="Cart"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <MdOutlineShoppingCart class="text-yellow-950" size={22} />
                  <span class="flex-1 ms-3 whitespace-nowrap text-amber-800">Cart</span>
                </a>
              </li>

              <li>
                <a
                  href="CustomerReview"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <VscFeedback class="text-yellow-950" size={22} />
                  <span class="ms-3 text-amber-800">Feedback</span>
                </a>
              </li>

            </div> : <></>}



            {role === "FoodSupplier" ? <div>

              <li>
                <a
                  href="/FoodItem"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <LuApple class="text-yellow-950" size={22} />
                  <span class="flex-1 ms-3 whitespace-nowrap text-amber-800" >Add Food Items</span>
                </a>
              </li>

              <li>
                <a
                  href="/EditFoodItem"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <FaRegEdit class="text-yellow-950" size={22} />
                  <span class="flex-1 ms-3 whitespace-nowrap text-amber-800" >Edit Food Items</span>
                </a>
              </li>

              <li>
                <a
                  href="/SupplierOrderList"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <TiThListOutline class="text-yellow-950" size={22} />
                  <span class="flex-1 ms-3 whitespace-nowrap text-amber-800">Supplier Orders</span>
                </a>
              </li>

              <li>
                <a
                  href="CustomerFeedback"
                  class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
                >
                  <VscFeedback class="text-yellow-950" size={22} />
                  <span class="ms-3 text-amber-800">Feedback</span>
                </a>
              </li>
            </div> : <></>}


            {role === "Admin" || role === "Customer" || role === "DeliveryPerson" ? <li>
              <a
                href="/OrderList"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
              >
                <TiThListOutline class="text-yellow-950" size={22} />
                <span class="flex-1 ms-3 whitespace-nowrap text-amber-800" >Order List</span>
              </a>
            </li> : <></>}



            {role === "Guest" ? <li>
              <a
                href="/Donor"
                class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
              >
                <BiDonateHeart class="text-yellow-950" size={22} />
                <span class="flex-1 ms-3 whitespace-nowrap text-amber-800">Donate Foods</span>
              </a>
            </li> : <></>}


            <li className="cursor-pointer">
              <a

                onClick={logout}
                class=" cursor-pointer flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-orange-100 group"
              >
                <MdLogout class="text-yellow-950" size={22} />
                <span class="flex-1 ms-3 whitespace-nowrap text-amber-800">Log out</span>
              </a>
            </li>


          </ul>
        </div>
      </aside>
    </div>
  );
}

export default Navbar;
