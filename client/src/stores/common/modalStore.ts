import { makeAutoObservable } from "mobx"

interface Modal {
    isOpen: boolean;
    body?: JSX.Element;
    onComplete?: Function
}

export default class ModalStore {
    modal: Modal = {
        isOpen: false,
        body: undefined,
        onComplete: undefined
    }

    constructor() {
        makeAutoObservable(this);
    }

    openModal = (content: JSX.Element, onComplete: Function) => {
        this.modal.isOpen = true;
        this.modal.body = content;
        this.modal.onComplete = onComplete;
    }

    closeModal = (isComplete: boolean) => {
        if(isComplete) {
            this.modal.onComplete?.();
        }
        this.modal.isOpen = false;
        this.modal.body = undefined;
        this.modal.onComplete = undefined;
    }
}