import { LOCAL_URL_PATH } from "@/constants/apiConstants";

export function getTokenAuth() {
  const token = localStorage.getItem("token");
  return token ? token : null;
}

export async function callApi<T, BodyType = unknown>({
  url,
  methodType,
  body,
}: {
  url: string;
  methodType: "GET" | "POST" | "PUT" | "PATCH" | "DELETE";
  body?: BodyType;
}): Promise<T> {
  try {
    const headers: Record<string, string> = {
      Accept: "application/json",
    };

    // ✅ Agregar token si existe
    const token = getTokenAuth();
    if (token) {
      headers["Authorization"] = `Bearer ${token}`;
    }

    const hasBody = ["POST", "PUT", "PATCH"].includes(methodType);
    if (hasBody) {
      headers["Content-Type"] = "application/json";
    }

    const response = await fetch(`${LOCAL_URL_PATH}${url}`, {
      method: methodType,
      headers,
      ...(hasBody && {
        body: JSON.stringify(body),
      }),
    });

    if (!response.ok) {
      throw new Error(`Error ${response.status}: ${response.statusText}`);
    }

    if (response.status === 204) {
      return {} as T;
    }

    return (await response.json()) as T;
  } catch (err) {
    console.error("API call error:", err);
    throw err;
  }
}
