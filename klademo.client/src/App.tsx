import * as React from 'react';
import './App.css';

function App() {
    const [number, setNumber] = React.useState('');
    const [resultNumber, setResultNumber] = React.useState('');

    const handleNumber = (event: React.FormEvent<HTMLInputElement>) => {
        setNumber(event.currentTarget.value);
    };

    const handleResultNumber = (value: string) => {
        setResultNumber(value);
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <input type="text" id="number" autoComplete="false" placeholder="enter number" value={number} onChange={handleNumber} />
                <br/>
                <button type="submit" >Submit</button>
            </form>

            <b>Result: </b><span>{resultNumber}</span>
        </div>
    );

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();

        const response = await fetch(`convert?number=${number}`);
        const data = await response.json();
        handleResultNumber(data);
    }
}

export default App;