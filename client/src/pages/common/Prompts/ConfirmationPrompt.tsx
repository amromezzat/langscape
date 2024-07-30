import { observer } from "mobx-react-lite"
import { Confirm } from "semantic-ui-react"
import { useStore } from "../../../stores/core/store";
import { useState } from "react";

export default observer(function ConfirmPrompt() {
    const {promptStore: {prompt, closePrompt,}} = useStore();
    const [isSubmitting, setIsLoading] = useState(false);

    function getConfirmButton() {
        const loadingParams = {
          loading: isSubmitting,
          disabled: isSubmitting
        };
        Object.assign(loadingParams, prompt.confirmButton);
      
        return loadingParams;
      }

    return <Confirm
        {...prompt}
        onConfirm={async () => {
            setIsLoading(true);
            await prompt.onConfirm?.();
            closePrompt();
            setIsLoading(false);
        }}
        onCancel={() => {
            prompt.onCancel?.();
            closePrompt();
        }}
        confirmButton={getConfirmButton()}
    />
})