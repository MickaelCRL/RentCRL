export const fetcherWithToken = async (
  url: string,
  token: string,
  method: "GET" | "POST",
  body?: any
) => {
  const res = await fetch(url, {
    method,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    ...(body ? { body: JSON.stringify(body) } : {}),
  });

  if (res.status === 404) {
    return null;
  }

  if (!res.ok) {
    const errorData = await res.json();
    throw new Error(errorData.message || "Erreur API");
  }

  return res.json();
};
