import { observer } from "mobx-react-lite";
import { Modal } from "semantic-ui-react";
import { useStore } from "../../../stores/core/store";

export default observer(function ModalContainer() {
    const {modalStore} = useStore();

    return (
        <Modal open={modalStore.modal.isOpen} onClose={modalStore.closeModal} size='mini'>
            <Modal.Content >
                {modalStore.modal.body}
            </Modal.Content>
        </Modal>
    )
})