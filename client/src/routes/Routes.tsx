import { Navigate, RouteObject, createBrowserRouter } from "react-router-dom"
import App from "../App"
import NotFound from "../pages/errors/NotFound"
import ServerError from "../pages/errors/ServerError"
import FlashCardSetDashboard from "../pages/flashcards/FlashCardSetDashboard"

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: 'flashcards', element: <FlashCardSetDashboard /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'server-error', element: <ServerError /> },
            { path: '*', element: <Navigate replace to='not-found' /> }
        ]
    }
]

export const router = createBrowserRouter(routes)