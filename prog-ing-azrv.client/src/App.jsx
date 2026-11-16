import { useState } from 'react';
import Login from './pages/Login.jsx';

function App() {
    const [page, setPage] = useState('login');

    if (page === 'register') {
        // Privremeno – ovdje dolazi Registracija (SCRUM-105)
        return (
            <div>
                <h1>Registracija</h1>
                <button onClick={() => setPage('login')}>Natrag na prijavu</button>
            </div>
        );
    }

    return <Login onGoToRegister={() => setPage('register')} />;
}

export default App;