window.adroit_users_CSV = '';

window.adroit_init = (unityInstance) => {
	// temp buttons for testing
	window.unityInstance = unityInstance;
	// const SendCsvCredsToUnityButton = document.getElementById('AdroitControls_send_CSV_credentials_to_Unity');
	// SendCsvCredsToUnityButton.onclick = () => {
	// 	window.adroit_send_CSV_credentials_to_Unity();
	// };
};

window.adroit_capture_profiler_data = () => {
	console.log('FROM WEB | window.adroit_capture_profiler_data');
	window.unityInstance.SendMessage('WebGLInterface', 'capture_profiler_data');
};

window.adroit_send_CSV_credentials_to_Unity = () => {
	console.log('FROM WEB | window.adroit_send_CSV_credentials_to_Unity');
	window.unityInstance.SendMessage('WebGLInterface', 'send_CSV_credentials_to_Unity', window.adroit_users_CSV);
};

(function adroit_get_CSV_From_URL() {
	const url = 'creds.csv';
	fetch(url)
		.then((response) => {
			if (!response.ok) {
				throw new Error(`HTTP error: ${response.status}`);
			}
			return response.text();
		})
		.then((text) => {
			window.adroit_users_CSV = text;
		})
		.catch((error) => {
			console.error(`Could not retrieve users csv file from ${url}`);
		});
}());
