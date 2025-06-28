import { useEffect, useState, useCallback } from "react";
import { LOCAL_URL_PATH } from "@/constants/apiConstants";

interface UseCallApiProps<BodyType = any> {
  url: string;
  methodType: "GET" | "POST" | "PUT" | "PATCH" | "DELETE";
  body?: BodyType;
}

export const getTokenAuth = () => {
  return localStorage.getItem("token") || null;
};

export default function useCallApi<T, BodyType = any>({
  url,
  methodType,
  body,
}: UseCallApiProps<BodyType>) {
  const [data, setData] = useState<T | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const refetch = useCallback(
    async (overrideBody?: BodyType) => {
      setLoading(true);
      setError(null);

      try {
        const token = getTokenAuth();

        const headers: Record<string, string> = {
          Accept: "application/json",
        };

        if (token) {
          headers["Authorization"] = `Bearer ${token}`;
        }

        const hasBody = ["POST", "PUT", "PATCH"].includes(methodType);
        if (hasBody) {
          headers["Content-Type"] = "application/json";
        }

        const fetchOptions: RequestInit = {
          method: methodType,
          headers,
        };

        if (hasBody) {
          fetchOptions.body = JSON.stringify(
            overrideBody !== undefined ? overrideBody : body
          );
        }

        const response = await fetch(`${LOCAL_URL_PATH}${url}`, fetchOptions);

        if (!response.ok) {
          throw new Error(`Error ${response.status}: ${response.statusText}`);
        }

        const result = (await response.json()) as T;
        setData(result);
      } catch (err) {
        setError(err as Error);
      } finally {
        setLoading(false);
      }
    },
    [url, methodType, JSON.stringify(body)]
  );

    useEffect(() => {
    const token = getTokenAuth(); 
    if (methodType === "GET" && token) {
      refetch(); 
    }
  }, [methodType, refetch]);


  return { data, error, loading, refetch };
}
