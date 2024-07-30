import { observer } from "mobx-react-lite";
import CardGroupComponent from "../../../components/features/flashCards/dashboard/CardGroupComponent";
import { useParams } from "react-router-dom";
import UserHeaderComponent from "../../../components/common/headers/UserHeaderComponent";

export default observer (function FlashCardSetDashboard() {
    const { username } = useParams();

    return (
        <>
            { username && (
                <UserHeaderComponent username={username} />
            )}
            <CardGroupComponent />
        </>
    )
});