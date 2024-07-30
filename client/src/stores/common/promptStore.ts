import { makeAutoObservable } from "mobx"
import { ConfirmProps } from "semantic-ui-react";

export interface Prompt extends ConfirmProps {
    onConfirm?: () => Promise<void>;
    onCancel?: () => void;
}

export default class PromptStore {
    prompt: Prompt = {open: false};

    constructor() {
        makeAutoObservable(this);
    }

    openPrompt = (prompt: Prompt) => {
        this.prompt = prompt;
    }

    closePrompt = () => {
        this.prompt = {open: false}
    }
}