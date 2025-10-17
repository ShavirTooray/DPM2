package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class LoginActivity : AppCompatActivity() {

    private lateinit var usernameInput: EditText
    private lateinit var passwordInput: EditText
    private lateinit var confirmPasswordInput: EditText
    private lateinit var loginButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        usernameInput = findViewById(R.id.editTextText7)
        passwordInput = findViewById(R.id.editTextTextPassword2)
        confirmPasswordInput = findViewById(R.id.editTextTextPassword3)
        loginButton = findViewById(R.id.button3)

        loginButton.setOnClickListener {
            val enteredUsername = usernameInput.text.toString().trim()
            val enteredPassword = passwordInput.text.toString().trim()
            val confirmedPassword = confirmPasswordInput.text.toString().trim()

            clearErrors()

            when {
                enteredUsername.isEmpty() -> {
                    usernameInput.error = "Required"
                    usernameInput.requestFocus()
                }
                enteredPassword.isEmpty() -> {
                    passwordInput.error = "Required"
                    passwordInput.requestFocus()
                }
                confirmedPassword.isEmpty() -> {
                    confirmPasswordInput.error = "Required"
                    confirmPasswordInput.requestFocus()
                }
                enteredPassword != confirmedPassword -> {
                    confirmPasswordInput.error = "Passwords do not match"
                    confirmPasswordInput.requestFocus()
                    Toast.makeText(this, "Passwords do not match", Toast.LENGTH_SHORT).show()
                }
                else -> {
                    val sharedPrefs = getSharedPreferences("LeaseLinkPrefs", MODE_PRIVATE)
                    val savedUsername = sharedPrefs.getString("username", null)
                    val savedPassword = sharedPrefs.getString("password", null)

                    if (enteredUsername == savedUsername && enteredPassword == savedPassword) {
                        Toast.makeText(this, "Login successful!", Toast.LENGTH_SHORT).show()
                        startActivity(Intent(this, DashboardActivity::class.java))
                        finish()
                    } else {
                        Toast.makeText(this, "Invalid credentials", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        }
    }

    private fun clearErrors() {
        usernameInput.error = null
        passwordInput.error = null
        confirmPasswordInput.error = null
    }
}