package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class SettingsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_settings)

        val userNameText = findViewById<TextView>(R.id.textView27)
        val changeSettingsText = findViewById<TextView>(R.id.textView28)
        val changePasswordText = findViewById<TextView>(R.id.textView29)
        val viewLeaseInfoText = findViewById<TextView>(R.id.textView32)
        val changePaymentMethodText = findViewById<TextView>(R.id.textView30)
        val logoutButton = findViewById<Button>(R.id.button13)

        // Load user's name from SharedPreferences
        val sharedPrefs = getSharedPreferences("LeaseLinkPrefs", MODE_PRIVATE)
        val userName = sharedPrefs.getString("name", "User")
        userNameText.text = userName

//        changeSettingsButton.setOnClickListener {
//            val intent = Intent(this, MoreSettingsActivity::class.java)
//            startActivity(intent)
//        }

//        changePasswordButton.setOnClickListener {
//            val intent = Intent(this, ChangePasswordActivity::class.java)
//            startActivity(intent)
//        }

//        viewLeaseInfoButton.setOnClickListener {
//            val intent = Intent(this, LeaseInfoActivity::class.java)
//            startActivity(intent)
//        }

//        changePaymentMethodButton.setOnClickListener {
//            val intent = Intent(this, ChangePaymentMethodActivity::class.java)
//            startActivity(intent)
//        }

        logoutButton.setOnClickListener {
            sharedPrefs.edit().clear().apply()
            Toast.makeText(this, "Logged out successfully", Toast.LENGTH_SHORT).show()
            val intent = Intent(this, MainActivity::class.java)
            intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
            startActivity(intent)
        }
    }
}
