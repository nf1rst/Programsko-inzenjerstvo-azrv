import { useState } from 'react';
import Login from './pages/Login.jsx';
import Register from './pages/Register.jsx';

function App() {
    const [page, setPage] = useState('login');
    const [successMessage, setSuccessMessage] = useState('');

    if (page === 'register') {
        return (
            <Register
                onGoToLogin={() => setPage('login')}
                onRegistered={() => {
                    setSuccessMessage('Registracija uspješna! Sada se možeš prijaviti.');
                    setPage('login');
                }}
            />
        );
    }

    return (
        <Login
            message={successMessage}
            onGoToRegister={() => {
                setSuccessMessage('');
                setPage('register');
            }}
        />
    );
}

export default App;
