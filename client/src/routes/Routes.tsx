import { Navigate, RouteObject, createBrowserRouter } from "react-router-dom"
import Home from "../pages/core/App"
import NotFound from "../pages/errors/NotFound"
import ServerError from "../pages/errors/ServerError"
import FlashCardSetDashboard from "../pages/features/flashcards/FlashCardSetDashboard"
import FlashCardSetItem from "../pages/features/flashcards/FlashCardSetItem"

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <Home />,
        children: [
            { path: 'sets', element: <FlashCardSetDashboard key='current' /> },
            { path: ':username/sets', element: <FlashCardSetDashboard key='other' />},
            { path: 'sets/:id', element: <FlashCardSetItem /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'server-error', element: <ServerError /> },
            { path: '*', element: <Navigate replace to='not-found' /> }
        ]
    }
]

export const router = createBrowserRouter(routes)