import { useState, useCallback } from "react";

const useHttp = () => {
  const [isLoading, setIsLoading] = useState(false);

  const sendRequest = useCallback(async (requestConfig) => {
    setIsLoading(true);

    const response = await fetch(requestConfig.url, {
      method: requestConfig.method ? requestConfig.method : "GET",
      headers: requestConfig.headers ? requestConfig.headers : {},
      body: requestConfig.body ? requestConfig.body : null,
    });
    let data = await response.json();

    if (!response.ok) {
      data = {
        ...data,
        hasError: true,
        errorMessage: data.message ? data.message : "Server error.",
      };
    }

    setIsLoading(false);
    return data;
  }, []);

  return {
    isLoading: isLoading,
    sendRequest: sendRequest,
  };
};

export default useHttp;
