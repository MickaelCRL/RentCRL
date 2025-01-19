import { useEffect, useState } from "react";

function App() {
  const [result, setResult] = useState<any[]>();

  useEffect(() => {
    const fetchData = async () => {
      const res = await fetch("http://localhost:5047/weatherforecast");
      const data = (await res.json()) as any[];
      setResult(data);
    };

    fetchData();
  }, []);
  return (
    <>
      <h1>Resultats</h1>

      {result?.map((el, index) =>
        el ? (
          <div key={index}>
            <h2>{el.date}</h2>
            <h3>{el.temperatureC}</h3>
            <h3>{el.temperatureF}</h3>
            <h3>{el.summary}</h3>
          </div>
        ) : (
          <div>null</div>
        )
      )}
    </>
  );
}

export default App;
