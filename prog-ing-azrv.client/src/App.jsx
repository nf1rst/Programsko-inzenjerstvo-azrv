import { useState } from 'react';
import Login from './pages/Login.jsx';
import Register from './pages/Register.jsx';
import Profile from './pages/Profile.jsx';

function App() {
    const [page, setPage] = useState('login');
    const [successMessage, setSuccessMessage] = useState('');

    if (page === 'profile') {
        return <Profile onSignedOut={() => { setSuccessMessage(''); setPage('login'); }} />;
    }
    if (page === 'register') {
        return <Register onGoToLogin={() => setPage('login')} onRegistered={() => {
            setSuccessMessage('Registracija uspješna! Sada se možeš prijaviti.');
            setPage('login');
        }} />;
    }
    return <Login message={successMessage} onLoggedIn={() => setPage('profile')}
        onGoToRegister={() => { setSuccessMessage(''); setPage('register'); }} />;
}

export default App;
