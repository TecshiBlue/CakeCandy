import { useEffect, useState, useCallback } from "react";
import { LOCAL_URL_PATH } from "@/constants/apiConstants";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
interface UseCallApiProps<BodyType = any> {
  url: string;
  methodType: "GET" | "POST" | "PUT" | "PATCH" | "DELETE";
  body?: BodyType;
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
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
        const headers: Record<string, string> = {
          Accept: "application/json",
        };

        const hasBody = ["POST", "PUT", "PATCH"].includes(methodType);
        if (hasBody) {
          headers["Content-Type"] = "application/json";
        }

        const response = await fetch(`${LOCAL_URL_PATH}${url}`, {
          method: methodType,
          headers,
          ...(hasBody && {
            body: JSON.stringify(
              overrideBody !== undefined ? overrideBody : body
            ),
          }),
        });

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
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [url, methodType, JSON.stringify(body)]
  );

  useEffect(() => {
    if (methodType === "GET") {
      refetch();
    }
  }, [methodType, refetch]);

  return { data, error, loading, refetch };
}