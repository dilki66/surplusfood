import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';

const EditFoodItemForm = ({ initialValues, onSubmit, onCancel }) => {

    return (
        <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={onSubmit}
        >
            {({ isSubmitting, setFieldValue }) => (
                <Form>
                    <div className="grid grid-cols-3 gap-2 mt-7">
                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Id <span className="text-red-700">*</span></label>
                            <Field type="text" value={itemId} name="id" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                            <ErrorMessage name="id" component="div" className="text-red-500 text-xs italic" />
                        </div>

                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Food Category <span className="text-red-700">*</span></label>
                            <select id="category" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                onChange={(event) => handleCategory(event, setFieldValue)}
                                value={foodcategory}>
                                <option>Select a category</option>
                                <option value="Rice and Curry">Rice and Curry</option>
                                <option value="Fried Rice">Fried Rice</option>
                                <option value="Noodles">Noodles</option>
                                <option value="string Hoppers">string Hoppers</option>
                                <option value="Pastry Items">Pastry Items</option>
                                <option value="Curries">Curries</option>
                                <option value="Drinks">Drinks</option>
                            </select>
                            <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                        </div>

                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Quantity <span className="text-red-700">*</span></label>
                            <Field type="text" name="quantity" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                            <ErrorMessage name="quantity" component="div" className="text-red-500 text-xs italic" />
                        </div>
                    </div>

                    <div className="grid grid-cols-3 gap-2">
                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Price <span className="text-red-700">*</span></label>
                            <Field type="text" name="price" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                            <ErrorMessage name="price" component="div" className="text-red-500 text-xs italic" />
                        </div>

                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Delivery <span className="text-red-700">*</span></label>
                            <select
                                id="servicetype"
                                className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                onChange={(event) => handleServiceType(event, setFieldValue)}
                                value={servicetype}
                            >
                                <option>Select a delivery</option>
                                <option value='1'>Delivery</option>
                                <option value="2">Self Pickup</option>
                                <option value="3">Delivery Free</option>
                            </select>
                            <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                        </div>

                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Food Status <span className="text-red-700">*</span></label>
                            <select
                                id="pricestatus"
                                className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                onChange={(event) => handlePriceStatus(event, setFieldValue)}
                                value={pricetatustype}
                            >
                                <option>Select a food status</option>
                                <option value="1">Free</option>
                                <option value="2">Discounted</option>
                                {/* priceStatus */}
                            </select>
                            <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                            {/* serviceTypeId */}
                        </div>
                    </div>

                    <div className="grid grid-cols-3 gap-2">
                        <div className="mb-4">
                            <label htmlFor="text" className="block text-gray-700 text-sm font-bold mb-2">Offers Available <span className="text-red-700">*</span></label>
                            <select id="countries" className="shadow appearance-none border rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                onChange={(event) => handleOffers(event, setFieldValue)}
                                value={offers}
                            >
                                <option>Select an offer</option>
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
                            </select>
                            <ErrorMessage name="text" component="div" className="text-red-500 text-xs italic" />
                        </div>
                    </div>
                    <button type="submit" disabled={isSubmitting}>
                        {isSubmitting ? 'Updating...' : 'Update Food Item'}
                    </button>
                    <button type="button" onClick={onCancel}>
                        Cancel
                    </button>
                </Form>
            )}
        </Formik>
    );
};

export default EditFoodItemForm;