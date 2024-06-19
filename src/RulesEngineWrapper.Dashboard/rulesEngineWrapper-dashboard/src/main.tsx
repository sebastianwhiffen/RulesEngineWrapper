import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./index.css";
import { AppConfig } from "./configuration/AppConfig";

const config = new AppConfig();

config.initialize()
  .then(() => {
    const router = createBrowserRouter([
      {
        path: config.getBaseUrl(),
        element: <div>Hello world!</div>,
      },
      {
        path: config.getBaseUrl() + "/holySmokes",
        element: <div>Hello world d d d </div>,
      },
    ]);

    console.log(config.getBaseUrl() ?? "No base URL found");

    ReactDOM.createRoot(document.getElementById("root")!).render(
      <React.StrictMode>
        <RouterProvider router={router} />
      </React.StrictMode>
    );
  })
  .catch((error) => {
    console.error("Failed to initialize application, check that the dashboard host is running:", error);
  });
