import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "bootstrap/dist/css/bootstrap.min.css";
import App from "./main/App.tsx";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const querClient = new QueryClient();
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <QueryClientProvider client={querClient}>
      <App />
    </QueryClientProvider>
  </StrictMode>,
);
